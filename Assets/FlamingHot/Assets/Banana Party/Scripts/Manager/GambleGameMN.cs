using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GambleGameMN : Singleton<GambleGameMN>
{
    public Action endGamble;

    public void SetEndGambleEvent(Action endGamble = null)
    {
        this.endGamble = endGamble;
    }

    public void ShowGamePanel(bool isShow = true)
    {
        
    }

    public  void PlayGambleGame()
    {

    }

    public void Collect()
    {
        ShowGamePanel(false);
        endGamble?.Invoke();
    }
}
