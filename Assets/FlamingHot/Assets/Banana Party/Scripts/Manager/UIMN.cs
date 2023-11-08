using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIMN : Singleton<UIMN>
{
    public GUIButton playBtn, autoPlayBtn, stopAutoBtn;
    [SerializeField] private Button reloadBtn, fullscreenBtn;
    [SerializeField] private Text gameNameTxt, balanceTxt, winTxt, betTxt, numberAutoPlayTxt;
    [SerializeField] private SettingPopup settingPopup;
    [SerializeField] private InfoPopup infoPopup;
    private bool isFullScreen;

    public Text lineBetTxt;
    public GUIButton infoBtn, settingBtn, bet;
    public GUIButton[] lines1, lines2; 

    private void Start() {
        EventSetting();
    }
    private void EventSetting()
    {
        reloadBtn.onClick.AddListener(Reload);
        fullscreenBtn.onClick.AddListener(FullScreen);
        playBtn.SetEvent(PlaySlot);
        autoPlayBtn.SetEvent(AutoPlayClick);
        stopAutoBtn.SetEvent(StopAutoPlayClick);

        infoBtn.SetEvent(delegate{UIMN.Instance.ShowPopup(PopupType.INFO);});
        settingBtn.SetEvent(delegate{ShowPopup(PopupType.SETTING);});
        bet.SetEvent(delegate{UIMN.Instance.ChangeBetValue(1); SetBetTxt();});
        SetLinesEvent();
    }

    public void UISetting() {
        GameNameSetting();
        BalanceSetting(0f);
        RewardSetting(0f);
        BetSetting();
    }

    private void GameNameSetting()
    {
        gameNameTxt.text = GameMN.Instance.gameData.gameName.ToUpper();
    }

    public void RefreshWinning()
    {
        GameMN.Instance.currentRewards = 0f;
        RewardSetting(0f);
    }

    public void BalanceSetting(float value)
    {
        float endValue = GameMN.Instance.userData.balance + value;
        StartCoroutine(Ultility.NumberRun(GameMN.Instance.userData.balance, endValue, balanceTxt));
        GameMN.Instance.userData.balance = endValue;
    }

    public void RewardSetting(float value)
    {
        float endValue = GameMN.Instance.currentRewards + value;
        StartCoroutine(Ultility.NumberRun(GameMN.Instance.currentRewards, endValue, winTxt));
        GameMN.Instance.currentRewards = endValue;
    }

    public void BetSetting()
    {
        betTxt.text = Ultility.GetMoneyFormated(GameMN.Instance.GetTotalBet());
        lineBetTxt.text = "LINE BET: " + Ultility.GetMoneyFormated(GameMN.Instance.GetBet());
        SetBetTxt();
    }
   
    public void PlaySlot()
    {
        GameMN.Instance.StartSpin();
    }

    private void AutoPlayClick()
    {
        GameMN.Instance.AutoPlay();
    }

    private void StopAutoPlayClick()
    {
        stopAutoBtn.Disable(true);
        GameMN.Instance.SetAutoPlay(false);
    }

    private void HideAllPopup()
    {
        settingPopup.ShowPopup(false);
        infoPopup.ShowPopup(false);
    }

    private void ShowPopup(PopupType popupType)
    {
        HideAllPopup();
        switch(popupType) 
        {
            case PopupType.SETTING:
                settingPopup.ShowPopup();
                break;
            case PopupType.INFO:
                infoPopup.ShowPopup();
                break;
        }
    }

    private void ChangeBetValue(int value)
    {
        GameMN.Instance.currentBetIndex += value ;
        if(GameMN.Instance.currentBetIndex >= GameMN.Instance.gameData.bets.Count)
            GameMN.Instance.currentBetIndex = 0;
        
        if(GameMN.Instance.currentBetIndex < 0)
            GameMN.Instance.currentBetIndex = GameMN.Instance.gameData.bets.Count - 1;
        
        BetSetting();
    }

    private void ChangeLineValueGUI(int value)
    {
        GameMN.Instance.currentLinesIndex = value;
        BetSetting();
    }

    public void ShowNumberAutoPlay()
    {
        numberAutoPlayTxt.text = GameMN.Instance.autoPlayData.GetNumberPlay().ToString();
    }

    public void ShowGUINormal()
    {
        DisableAll(false);
    }

    public void ShowGUINormalSpin()
    {
        DisableAll(true);
    }

    public void ShowGUIAutoSpin()
    {
        DisableAll(true);
        stopAutoBtn.Show(true);
    }

    private void Reload()
    {

    }

    private void FullScreen()
    {

    }

    private void SetLinesEvent()
    {
        for(int i = 0; i < lines1.Length; i++)
        {
            int n = i;
            lines1[i].SetEvent(delegate{UIMN.Instance.ChangeLineValueGUI(n); HighlightLineValue(n);});
            lines2[i].SetEvent(delegate{UIMN.Instance.ChangeLineValueGUI(n); HighlightLineValue(n);});
        }
    }

    public void HighlightLineValue(int index)
    {
        foreach(GUIButton button in lines1)
        {   
            button.SetTextColor(Color.white);
        }

        foreach(GUIButton button in lines2)
        {   
            button.SetTextColor(Color.white);
        }

        lines1[index].SetTextColor(Color.yellow);
        lines2[index].SetTextColor(Color.yellow);
    }

    private void SetBetTxt()
    {
        bet.SetTxt(Ultility.GetMoneyFormated(GameMN.Instance.GetBet()));
    }

    private void DisableAll(bool isDisable)
    {
        foreach(GUIButton button in lines1)
        {   
            button.Disable(isDisable);
        }

        foreach(GUIButton button in lines2)
        {   
            button.Disable(isDisable);
        }

        infoBtn.Disable(isDisable);
        settingBtn.Disable(isDisable);
        bet.Disable(isDisable);
        playBtn.Disable(isDisable);
        stopAutoBtn.Show(false);
    }

}

public enum PopupType
{
    SETTING, INFO, HISTORY, CHANGE_BET, AUTO_PLAY
}

public enum AutoPlayType
{
   AUTO, STOP
} 
