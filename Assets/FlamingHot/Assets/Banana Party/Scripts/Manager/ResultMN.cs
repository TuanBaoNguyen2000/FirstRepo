using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultMN : Singleton<ResultMN>
{
    public SymbolResult symbolResult;
    private List<int> symbolOccurList = new List<int>();
    public List<WinData> winDatas = new List<WinData>();
    public bool isLineWin;

    public void CreateSymbolOccurList()
    {
        symbolOccurList = Ultility.CreateSymbolOccurList(GameMN.Instance.gameData.symbols);
    }

    public bool HaveResultToRun()
    {
        Ultility.ShuffleIntList(symbolOccurList);

        int column = GameMN.Instance.gameData.column;
        int row = GameMN.Instance.gameData.row;
        SymbolResult symResult = new SymbolResult(column, row, symbolOccurList);
        symbolResult = symResult;

        isLineWin = Ultility.isWin(GameMN.Instance.gameData.winOccur.lineWin);

        winDatas = new List<WinData>();
        if(isLineWin == IsHaveLineWin())
            return true;
            
        return false;
    }

    private bool IsHaveLineWin()
    {
        List<Combo> combos = new List<Combo>();
        for(int i = 0; i < GameMN.Instance.GetLine(); i++)
        {
            if(i >= GameMN.Instance.GetLine())
                break;
                
            WinningLine line = GameMN.Instance.gameData.winningLines[i];
            Combo combo = new Combo();
            for(int j = 0; j < line.positions.Count; j++)
            {
                int symbol = symbolResult.GetSymbol(j, line.positions[j]);
                combo.array[j] = symbol;
            }

            combos.Add(combo);
        }
        CheckCombo(combos);
        return winDatas.Count > 0;
    }

    private void CheckCombo (List<Combo> combos)
    {
        for(int i = 0 ; i < combos.Count; i++)
        {
            int firstSymbol = 0;
            int matchCount = 0;
            for(int j = 0; j < combos[i].array.Length; j++)
            {
                int symbol = combos[i].array[j];
                if(matchCount == 0)
                {
                    firstSymbol = symbol;
                    matchCount ++;
                }
                else
                {
                    if(firstSymbol == symbol)
                        matchCount ++;
                    else
                    {
                        SymbolData firstSymData = GameMN.Instance.gameData.symbols[firstSymbol];
                        SymbolData nextSymData = GameMN.Instance.gameData.symbols[symbol];

                        if(firstSymData.type == SymbolType.SCATTER && nextSymData.type == SymbolType.WILD)
                            break;

                        if(firstSymData.type == SymbolType.WILD)
                        {
                            if(nextSymData.type == SymbolType.SCATTER)
                                break;
                            
                            if(matchCount <= 3)
                            {
                                 if(firstSymData.type == SymbolType.WILD && nextSymData.type == SymbolType.NORMAL)
                                {
                                    firstSymbol = symbol;
                                    matchCount ++;
                                }
                            }
                        }
                        else if(nextSymData.type == SymbolType.WILD)
                            matchCount ++;
                        else
                            break;
                    } 
                }
            }
            CreateWindatas(firstSymbol, matchCount, i);
        }
    }

    private void CreateWindatas(int firstSymbol, int matchCount, int line)
    {
        
        float reward = GameMN.Instance.gameData.symbols[firstSymbol].rewards[matchCount - 1] * GameMN.Instance.GetBet();
        if(reward > 0)
        {
            WinData data = new WinData();
            data.line = line;
            data.symbolCount = matchCount;
            data.symbol = firstSymbol;
            data.lineReward = reward;
            winDatas.Add(data);
        }
    }
    
    public float GetLineReward()
    {
        float reward = 0f;
        foreach(WinData data in winDatas)
        {
            reward += data.lineReward;
        }
        return reward;
    }
}
