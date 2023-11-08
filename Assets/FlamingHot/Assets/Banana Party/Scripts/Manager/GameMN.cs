using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMN : Singleton<GameMN>
{
   public GameData gameData;
   public UserData userData;
   public MachineData machineData;
   public List<GameHistory> historyList = new List<GameHistory>();
   public GameHistory currentHistory;
   public BetType betType = BetType.NORMAL;
   public GameType gameType = GameType.NORMAL;
   public GameActivity gameActivity = GameActivity.NORMAL;
   public SpinType spinType = SpinType.NORMAL_SPIN;
   public List<string> currency = new List<string>();
   public float currentRewards;
   public int currentBetIndex = 0, currentLinesIndex = 0, currentAutoIndex = 0;
   public AutoPlayData autoPlayData;
   public bool isHaveGambleGame = false;

   private void Start() {
      Application.targetFrameRate = 60;
      GameSetting();
   }

   private void GameSetting()
   {
      userData.balance = 10000f;
      machineData.balance = 1000f;
      currentLinesIndex = gameData.lineCountList.Count - 1;
      UIMN.Instance.UISetting();
      ResultMN.Instance.CreateSymbolOccurList();
   }

   private void GiveReward()
   {
      BalanceSetting(currentRewards);
      MachineBalanceSetting(-currentRewards);
   }

   public void BalanceSetting(float value)
   {
      UIMN.Instance.BalanceSetting(value);
   }

   public void MachineBalanceSetting(float value)
   {
      machineData.balance += value;
   }

   public void StartSpin()
   {
      if(gameActivity != GameActivity.NORMAL) 
         return;

      if(!EnoughBalance())
         return;
         
      UIMN.Instance.RefreshWinning();

      if(!isAutoPlay())
         UIMN.Instance.ShowGUINormalSpin();
      else
         UIMN.Instance.ShowGUIAutoSpin();

      SlotMN.Instance.RunSlots();
   }

   public void AutoPlay()
   {
      if(gameActivity != GameActivity.NORMAL) 
         return;

      if(!EnoughBalance())
      {
         UIMN.Instance.ShowGUINormal();
         return;
      }

      SetAutoPlay(true);
      UIMN.Instance.ShowGUIAutoSpin();
      if(isAutoPlay() && gameActivity == GameActivity.NORMAL)
      {
         SetSpinType(SpinType.AUTO_SPIN);
         StartSpin();
      }
   }

   public void SpinStop()
   {
      StartCoroutine(IESpinStop());
   }

   private IEnumerator IESpinStop()
   {
      yield return new WaitForSeconds(0.3f);
      ShowWin();

      //PLAY GAMBLE
      yield return IEShowGambleGame();
      
      //END
      yield return IEAllEnd();
   }

   private void ShowWin()
   {
      GameStatus(GameActivity.NORMAL);
      ShowWinMN.Instance.ShowAllWin(true);
      UIMN.Instance.RewardSetting(ResultMN.Instance.GetLineReward());
   }

   public virtual IEnumerator IEShowGambleGame()
   {
      bool gambleEnd = false;
      if(isHaveGambleGame)
      {
         yield return new WaitForSeconds(1f);
         GambleGameMN.Instance.ShowGamePanel();
         GambleGameMN.Instance.SetEndGambleEvent(() => {gambleEnd = true; });

         while (!gambleEnd) 
            yield return new WaitForSeconds(0.1f);
      }
   }

   private IEnumerator IEAllEnd()
   {
      GiveReward();
      EndSpinHistory();

      if(!autoPlayData.isStop()) 
      {
         yield return new WaitForSeconds(1f);
         UIMN.Instance.PlaySlot();
         UIMN.Instance.ShowNumberAutoPlay();
      }
      else
      {
         yield return new WaitForSeconds(0.25f);
         UIMN.Instance.ShowGUINormal();
      }
   }

   public void StartSpinHistory()
   {
      GameHistory newHistory = new GameHistory();
      newHistory.StartSpinData(betType, GetTotalBet(), userData.balance);
      historyList.Add(newHistory);
      currentHistory = newHistory;
   }

   public void EndSpinHistory()
   {
      currentHistory.EndSpinData(currentRewards, userData.balance, ResultMN.Instance.symbolResult, ResultMN.Instance.winDatas);
   }

   public float GetTotalBet()
   {
      return gameData.notChangeLineCount ? GetBet() : GetBet() * GetLine() * GetMultipler();
   }
   
   public float GetBet()
   {
      return gameData.bets[currentBetIndex] * GetMultipler();
   }

   public int GetLine()
   {
      return gameData.lineCountList[currentLinesIndex];
   }

   public int GetNumberAuto()
   {
      return gameData.NumberAutoPlay[currentAutoIndex];
   }

   public float GetMultipler()
   {
      return betType == BetType.FORTUNE_BET ? gameData.betMultipler : 1;
   }

   public bool EnoughBalance()
   {
      if(userData.balance >= GetTotalBet())
      {
         StartSpinHistory();
         BalanceSetting(-GetTotalBet());
         MachineBalanceSetting(GetTotalBet());
         return true;
      }
      return false;
   }

   public bool isAutoPlay()
   {
      return autoPlayData.isAutoPlay;
   }
   
   public void GameStatus(GameActivity gameActivity)
   {
      this.gameActivity = gameActivity;
   }

   public void SetSpinType(SpinType spinType)
   {
      this.spinType = spinType;
   }

   public void SetAutoPlay(bool isAutoPlay)
   {
      autoPlayData.isAutoPlay = isAutoPlay;
   }
}

[System.Serializable] 
public class UserData
{
   public float balance = 0f;
}

[System.Serializable] 
public class MachineData
{
   public float balance = 0f;
}

[System.Serializable] 
public class GameHistory
{
   public TimeData timeData;
   public BetType betType;
   public float totalBet;
   public float totalWin;
   public float balanceBefore;
   public float balanceAfter;
   public List<WinData> winDatas = new List<WinData>();
   public SymbolResult symbolResult;

   public void StartSpinData(BetType betType, float totalBet, float balanceBefore)
   {
      DateTime time = DateTime.Now;
      this.timeData = new TimeData(time.Day, time.Month, time.Year, time.Hour, time.Minute);;
      this.betType = betType;
      this.totalBet = totalBet;
      this.balanceBefore = balanceBefore;
   }

   public void EndSpinData(float totalWin, float balanceAfter, SymbolResult symbolResult, List<WinData> winDatas)
   {
      this.totalWin = totalWin;
      this.balanceAfter = balanceAfter;
      this.symbolResult = symbolResult;
      this.winDatas = winDatas;
   }
}

public enum GameActivity
{
   NORMAL, SPIN, GAMBLE_GAME, BONUS_GAME, AUTO_PLAY
}

public enum GameType
{
   NORMAL, GAMBLE, BONUS
}

public enum GambleResult
{
   WIN, LOSE
}

public enum BetType
{
   NORMAL, FORTUNE_BET
}

public enum SpinType
{
   NORMAL_SPIN, AUTO_SPIN, FREE_SPIN
}

[System.Serializable] 
public class TimeData
{
   public int day, month, year, hour, minute;

   public TimeData(int day, int month, int year, int hour, int minute)
   {
      this.day = day;
      this.month = month;
      this.year = year;
      this.hour = hour;
      this.minute = minute;
   }

   public string Str()
   {
      return day + "/" + month + "/" + year + " " + hour + ":" + minute;
   }
}

public class SymbolResult
{
   public int[,] symbolResult;

   public SymbolResult()
   {

   }

   public SymbolResult(int column, int row, List<int> symbolOccurList)
   {
      int symbolIndex = 0;
      symbolResult = new int[column, row];
      for(int i = 0; i < column; i++)
      {
         for(int j = 0 ; j < row ; j++)
         {
               symbolResult[i,j] = symbolOccurList[symbolIndex];
               symbolIndex ++;
         }
      }
   }

   public void Debugs(int column, int row){
      for(int i = 0; i < column; i++)
      {
         for(int j = 0 ; j < row ; j++)
         {
            Debug.Log("Column: " + i + "_" + "row: " + j + "_" + "Symbol: " + symbolResult[i,j]);
         }
      }
   }

   public int GetSymbol(int column, int row)
   {
      return symbolResult[column, row];
   }

   public void SetSymbol(int column, int row, int symbol)
   {
      symbolResult[column, row] = symbol;
   }
}

[System.Serializable]
public class WinData
{
    public int line = -1, symbol = -1, symbolCount = 0;
    public float lineReward = 0, bonusReward = 0;
    public int freeSpinReward = 0;
}

[System.Serializable]
public class AutoPlayData
{
   public bool isAutoPlay = false;
   public int numberPlay = 0, currentNumberPlay = 0;
   public float lossLimit = 0f, winLimit = 0f;
   public float currentloss= 0f, currentWin = 0f;

   public void ValueSetting(bool isAutoPlay ,int numberPlay, float lossLimit = 0, float winLimit = 0)
   {
      this.isAutoPlay = isAutoPlay;
      this.numberPlay = numberPlay;
      this.lossLimit = lossLimit;
      this.winLimit = winLimit;
   }

   public bool isStop()
   {
      bool _isStop = false;
      
      if(!isAutoPlay)
         _isStop = true;
      
      float reward = GameMN.Instance.currentRewards - GameMN.Instance.GetTotalBet();
      currentloss -= reward;
      currentWin += reward;
      currentNumberPlay++;
      
      if(lossLimit > 0 && currentloss >= lossLimit)
         _isStop = true;

      if(winLimit > 0 && currentWin >= winLimit)
         _isStop = true;

      if(numberPlay > 0 && currentNumberPlay >= numberPlay)
         _isStop = true;
      
      if(_isStop) 
         Refresh();

      return _isStop;
   }

   public void Refresh()
   {
      isAutoPlay = false;
      numberPlay = 0; 
      currentNumberPlay = 0;
      lossLimit = 0f; 
      winLimit = 0f;
      currentloss= 0f; 
      currentWin = 0f;
   }

   public int GetNumberPlay()
   {
      return numberPlay -= currentNumberPlay;
   }
}

