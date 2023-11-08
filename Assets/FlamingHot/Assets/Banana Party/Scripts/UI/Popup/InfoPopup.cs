using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPopup : Popups
{
    [Header("Paytable Page")]
    [SerializeField] List<SymbolInfo> symInfoList = new List<SymbolInfo>();
    
    [Header("WinningLines Page")]
    [SerializeField] private LineInfo lineInfoPrefab;
    [SerializeField] private Transform lineInfoHolder;

    [Header("Pages")]
    public List<GameObject> pageList = new List<GameObject>();
    public int pageIndex = 0; 
    
    private void Start() 
    {
        Init();
    }

    private void Init()
    {
        PaytableSetting();
        WinningLinesSetting();
    }

    public virtual void ChangePageSetting()
    {
        
    }

    public override void ShowPopup(bool isShow = true)
    {
        base.ShowPopup(isShow);
        if(!isShow) return;
        ShowFirstPage();
        ShowPage();
        RefreshSymbolInfoRewards();
    } 

    public virtual void ShowFirstPage()
    {
        pageIndex = 0; 
    }

    public void WinningLinesSetting()
    {
        for(int i = 0; i < GameMN.Instance.gameData.winningLines.Count; i++)
        {
            WinningLine data = GameMN.Instance.gameData.winningLines[i];
            LineInfo line = Instantiate(lineInfoPrefab, lineInfoHolder);
            line.Setting(i + 1, data.sprite);
        }
    }

    public void PaytableSetting()
    {
        for(int i = 0; i < GameMN.Instance.gameData.symbols.Count; i++)
        {
            SymbolData data = GameMN.Instance.gameData.symbols[i];
            SymbolInfo sym = symInfoList[i];
            sym.Setting(data);
            sym.ShowRewards();
        }
    }

    private void RefreshSymbolInfoRewards()
    {
        foreach(SymbolInfo sym in symInfoList)
        {
            sym.ShowRewards();
        }
    }

    public void ChangePageIndex(int index)
    {
        pageIndex = index;
        ShowPage();
    }

    private void ShowPage()
    {
        HideAllPage();
        pageList[pageIndex].SetActive(true);
    }

    private void HideAllPage(){
        foreach(GameObject page in pageList)
        {
            page.SetActive(false);
        }
    }
}
