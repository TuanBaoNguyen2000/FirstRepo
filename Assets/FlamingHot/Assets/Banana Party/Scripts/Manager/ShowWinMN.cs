using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWinMN : Singleton<ShowWinMN>
{
    public float timeToShowOneWin = 3f;
    public int dataIndex = 0;

    public void ShowAllWin(bool isShow)
    {
        if(isShow) 
        {
            dataIndex = 0;
            HideAllWin();
            for(int i = 0; i < ResultMN.Instance.winDatas.Count; i++)
            {
                WinData data = ResultMN.Instance.winDatas[i];
                LineWinning(data);
            }

            Invoke("ShowOnceWin", timeToShowOneWin);
        }
        else
        {
           CancelInvoke("ShowOnceWin");
        }
    }

    public void HideAllWin()
    {
        int column = GameMN.Instance.gameData.column;
        int row = GameMN.Instance.gameData.row;
        for(int i = 0; i < column; i++)
        {
            for(int j = 0 ; j < row ; j++)
            {
                Symbol symbol = SlotMN.Instance.GetSymbol(i, j);
                symbol.WinSetting(false, Color.white);
                symbol.SetBlurIcon(false);
            }
        }
    }

    public void ShowOnceWin()
    {
        if(ResultMN.Instance.winDatas.Count < 1)
        {
            CancelInvoke("ShowOnceWin");
            return;
        }

        HideAllWin();
        WinData data = ResultMN.Instance.winDatas[dataIndex];
        LineWinning(data);
        dataIndex ++;
        if(dataIndex > ResultMN.Instance.winDatas.Count - 1) 
            dataIndex = 0;
        Invoke("ShowOnceWin", timeToShowOneWin);
    }

    public void LineWinning(WinData data)
    {
        if(data.line != -1)
        {
            WinningLine line = GameMN.Instance.gameData.winningLines[data.line];
            for(int j = 0; j < line.positions.Count; j++)
            {
                if(j <= data.symbolCount - 1)
                {
                    Symbol symbol = SlotMN.Instance.GetSymbol(j,line.positions[j]);
                    symbol.WinSetting(true, Color.yellow);
                }
            }
        }
    }
}
