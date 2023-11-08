using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.Events;

public class SlotMN : Singleton<SlotMN>
{
    [Header("Reference")]
    public GameObject tilePrefab;
    public int slotTilesCount = 30;

    [Header("Reel")]
    public List<SlotReel> reels = new List<SlotReel>();
    public List<Transform> reelsParent = new List<Transform>();
    public SlotReel reelPrefab;

    [Header("Var")]
    public SpinValue spinValue;
    private bool slotsRunned = false;
    public float radiusPlus = 0f;
    
    private void OnValidate()
    {
        Validate();
    }

    void Validate()
    {
        spinValue.inRotTime = Mathf.Clamp(spinValue.inRotTime, 0, 1f);
        spinValue.inRotAngle = Mathf.Clamp(spinValue.inRotAngle, 0, 10);

        spinValue.outRotTime = Mathf.Clamp(spinValue.outRotTime, 0, 1f);
        spinValue.outRotAngle = Mathf.Clamp(spinValue.outRotAngle, 0, 10);
    }
    
    void Start()
    {
        CreateReels();
    }

    private void CreateReels()
    {
        for(int i = 0; i < GameMN.Instance.gameData.column; i++)
        {
            SlotReel reel = Instantiate(reelPrefab, reelsParent[i]);
            reel.Init(i, slotTilesCount);
            reels.Add(reel);
        }
    }

    public virtual void RunSlots()
    {
        if (GameMN.Instance.gameActivity != GameActivity.NORMAL) return;
        GameMN.Instance.GameStatus(GameActivity.SPIN);
        ShowWinMN.Instance.ShowAllWin(false);

        StopCoroutine(IERunSlots());
        StartCoroutine(IERunSlots());
    }

    private IEnumerator IERunSlots()
    {
        while (!ResultMN.Instance.HaveResultToRun()) 
            yield return null;

        bool endRotated = false;
        RotateSlots(() => { endRotated = true; });

        while (!endRotated) 
            yield return new WaitForSeconds(0.1f);
        
        yield return new WaitForSeconds(0.1f);
        GameMN.Instance.SpinStop();
    }

    private void RotateSlots(Action rotCallBack)
    {
        ParallelTween pT = new ParallelTween();
        
        for (int i = 0; i < reels.Count; i++)
        {
            int n = i;

            pT.Add((callBack) =>
            {
                reels[n].ReelMove(spinValue, callBack);
            });
        }

        pT.Start(rotCallBack);
    }

    public static int GetSymbolCount()
    {
        return GameMN.Instance.gameData.symbols.Count;
    }

    public Symbol GetSymbol(int column, int row)
    {
        return reels[column].symbols[row];
    }

    public virtual void TurboSetup(bool isTurbo)
    {

    }
}

[System.Serializable]
public class SpinValue
{
    public EaseAnim inRotType = EaseAnim.EaseLinear; // in rotation part
    public float inRotTime = 0.3f;
    public float inRotAngle = 7;
    public EaseAnim outRotType = EaseAnim.EaseLinear;   // out rotation part
    public float outRotTime = 0.3f;
    public float outRotAngle = 7;
    public EaseAnim mainRotateType = EaseAnim.EaseLinear;   // main rotation part
    public float mainRotateTime = 4f;
    public float mainRotateTimeNormal = 4f;
    public float mainRotateTimeTurbo = 1f;
    public float endRotated = -360f;
}