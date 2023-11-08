using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopup : MonoBehaviour
{
    public Button goToLobbyBtn, exitBtn;
    public Toggle soundTg;
    [SerializeField] private GameObject panel;

    public Button betMaxBtn, lineMaxBtn, autoPlayMaxBtn;
    public Button betPlusBtn, betMinusBtn, linePlusBtn, lineMinusBtn, autoPlayPlusBtn, autoPlayMinusBtn;
    public Text betTxt, lineTxt, autoPlayTxt;
    public Button autoSpin;
    private int numberAuto = 5; 
    private int autoIndex = 0;

    void Awake() 
    {
        SettingEvent();
    }

    private void SettingEvent()
    {
        exitBtn.onClick.AddListener(delegate{ShowPopup(false);});
        goToLobbyBtn.onClick.AddListener(GotoLobby);

        betMaxBtn.onClick.AddListener(delegate{ChangeBet(-1, isMax: true);});
        lineMaxBtn.onClick.AddListener(delegate{ChangeLines(-1, isMax: true);});
        autoPlayMaxBtn.onClick.AddListener(delegate{ChangeNumberAuto(-1, isMax: true);});

        betPlusBtn.onClick.AddListener(delegate{ChangeBet(1);});
        betMinusBtn.onClick.AddListener(delegate{ChangeBet(-1);});

        linePlusBtn.onClick.AddListener(delegate{ChangeLines(1);});
        lineMinusBtn.onClick.AddListener(delegate{ChangeLines(-1);});

        autoPlayPlusBtn.onClick.AddListener(delegate{ChangeNumberAuto(1);});
        autoPlayMinusBtn.onClick.AddListener(delegate{ChangeNumberAuto(-1);});

        autoSpin.onClick.AddListener(AutoPlay);
    }

    public void GotoLobby()
    {

    }

    public void ShowPopup(bool isShow = true)
    {
        panel.SetActive(isShow);
        lineTxt.text = GameMN.Instance.gameData.lineCountList[GameMN.Instance.currentLinesIndex].ToString();
        betTxt.text = GameMN.Instance.gameData.bets[GameMN.Instance.currentBetIndex].ToString();
        autoPlayTxt.text = GameMN.Instance.gameData.NumberAutoPlay[autoIndex].ToString();
    }

    private void ChangeLines(int value, bool isMax = false)
    {
        if(isMax)
        {
            GameMN.Instance.currentLinesIndex = GameMN.Instance.gameData.lineCountList.Count - 1;
            lineTxt.text = GameMN.Instance.GetLine().ToString();
            return;
        }

        GameMN.Instance.currentLinesIndex += value;

        if(GameMN.Instance.currentLinesIndex >= GameMN.Instance.gameData.lineCountList.Count)
            GameMN.Instance.currentLinesIndex = 0;
        
        if(GameMN.Instance.currentLinesIndex < 0)
            GameMN.Instance.currentLinesIndex = GameMN.Instance.gameData.lineCountList.Count - 1;
        
        lineTxt.text = GameMN.Instance.GetLine().ToString();
        UIMN.Instance.HighlightLineValue(GameMN.Instance.currentLinesIndex);
        UIMN.Instance.BetSetting();
    }

    private void ChangeBet(int value, bool isMax = false)
    {
        if(isMax)
        {
            GameMN.Instance.currentBetIndex = GameMN.Instance.gameData.bets.Count - 1;
            betTxt.text = GameMN.Instance.gameData.bets[GameMN.Instance.currentBetIndex].ToString();
            return;
        }

        GameMN.Instance.currentBetIndex += value;

        if(GameMN.Instance.currentBetIndex >= GameMN.Instance.gameData.bets.Count)
            GameMN.Instance.currentBetIndex = 0;
        
        if(GameMN.Instance.currentBetIndex < 0)
            GameMN.Instance.currentBetIndex = GameMN.Instance.gameData.bets.Count - 1;
        
        betTxt.text = GameMN.Instance.gameData.bets[GameMN.Instance.currentBetIndex].ToString();
        UIMN.Instance.BetSetting();
    }

    private void ChangeNumberAuto(int value, bool isMax = false)
    {
        if(isMax)
        {
            autoIndex = GameMN.Instance.gameData.NumberAutoPlay.Count - 1;
            autoPlayTxt.text = GameMN.Instance.gameData.NumberAutoPlay[autoIndex].ToString();
            return;
        }

        autoIndex += value;

        if(autoIndex >= GameMN.Instance.gameData.NumberAutoPlay.Count)
            autoIndex = 0;
        
        if(autoIndex < 0)
            autoIndex = GameMN.Instance.gameData.NumberAutoPlay.Count - 1;
        
        numberAuto = GameMN.Instance.gameData.NumberAutoPlay[autoIndex];

        autoPlayTxt.text = GameMN.Instance.gameData.NumberAutoPlay[autoIndex].ToString();
    }

    private void AutoPlay()
    {
        ShowPopup(false);
        GameMN.Instance.autoPlayData.ValueSetting(true, numberAuto);
        UIMN.Instance.PlaySlot();
        UIMN.Instance.ShowNumberAutoPlay();
        GameMN.Instance.SetSpinType(SpinType.AUTO_SPIN);
    }
}
