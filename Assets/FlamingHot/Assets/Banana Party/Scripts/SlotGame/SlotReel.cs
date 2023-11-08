using UnityEngine;
using System;
using System.Collections.Generic;

public class SlotReel : MonoBehaviour
{
    public RayCaster[] rayCasters;
    public Symbol[] symbols;
    public Transform TilesGroup;
    public TweenSeq tS;
    public float spinStartDelay = 0f, tileSizeY = 3.13f, gapY = 0.35f;
    public int spinStartRandomize = 0, spinSpeedMultiplier = 1, windowSize, reelIndex = 0;
    public float anglePerTileRad = 0, anglePerTileDeg = 0;
    public float addAnglePerTileDeg , addAnglePerTileRad, radius;
    public int slotTilesCount;
    public int symbolLayer = 10;

    private void OnValidate()
    {
        spinStartRandomize = (int)Mathf.Clamp(spinStartRandomize, 0, 20);
        spinStartDelay = Mathf.Max(0,spinStartDelay);
        spinSpeedMultiplier = Mathf.Max(0, spinSpeedMultiplier);
    }

    private void OnDestroy()
    {
        CancelRotation();
    }

    private void OnDisable()
    {
        CancelRotation();
    }
    
    public void Init(int index, int slotTilesCount)
    {
        this.slotTilesCount = slotTilesCount;
        reelIndex = index;
        symbols = new Symbol[slotTilesCount];
        CreateReel();
        CaculateReelGeometry();
        OrientToBaseRC();
        CreateReelTiles();
        InitSymbols();
    }

    private void CreateReel()
    {
        TilesGroup = (new GameObject()).transform;
        TilesGroup.localScale = transform.lossyScale;
        TilesGroup.parent = transform;
        TilesGroup.localPosition = Vector3.zero ;
        TilesGroup.name = "Reel(" + name + ")"; 
    }

    private void CaculateReelGeometry()
    {
        // calculate reel geometry
        float distTileY = tileSizeY + gapY; //old float distTileY = 3.48f;

        anglePerTileDeg = 360.0f / (float)slotTilesCount;
        anglePerTileRad = anglePerTileDeg * Mathf.Deg2Rad;
        radius = (distTileY / 2f) / Mathf.Tan(anglePerTileRad / 2.0f) + SlotMN.Instance.radiusPlus; //old float radius = ((tileCount + 1) * distTileY) / (2.0f * Mathf.PI);

        windowSize = rayCasters.Length;

        bool isEvenRayCastersCount = (windowSize % 2 == 0);
        int dCount = (isEvenRayCastersCount) ? windowSize / 2 - 1 : windowSize / 2;
        addAnglePerTileDeg = (isEvenRayCastersCount) ? -anglePerTileDeg*dCount - anglePerTileDeg /2f : -anglePerTileDeg;
        addAnglePerTileRad = (isEvenRayCastersCount) ? -anglePerTileRad*dCount - anglePerTileRad /2f : -anglePerTileRad;
        TilesGroup.localPosition = new Vector3(TilesGroup.localPosition.x, TilesGroup.localPosition.y, radius); // offset reel position by z-coordinat
    }

    private void OrientToBaseRC()
    {
        // orient to base rc
        RayCaster baseRC = rayCasters[rayCasters.Length - 1]; // bottom raycaster
        float brcY = baseRC.transform.localPosition.y;
        float dArad = 0f;
        if (brcY >-radius && brcY < radius)
        {
            float dY = brcY - TilesGroup.localPosition.y;
            dArad = Mathf.Asin(dY/radius);
            addAnglePerTileRad = dArad;
            addAnglePerTileDeg = dArad * Mathf.Rad2Deg;
        }
    }

    private void CreateReelTiles()
    {
        for (int i = 0; i < slotTilesCount; i++)
        {
            float n = (float)i;
            float tileAngleRad = n * anglePerTileRad + addAnglePerTileRad;
            float tileAngleDeg = n * anglePerTileDeg + addAnglePerTileDeg;

            symbols[i] = Instantiate(SlotMN.Instance.tilePrefab, TilesGroup).GetComponent<Symbol>();
            symbols[i].transform.localPosition = new Vector3(0, radius * Mathf.Sin(tileAngleRad), -radius * Mathf.Cos(tileAngleRad));
            symbols[i].transform.localEulerAngles = new Vector3(tileAngleDeg, 0, 0);
            symbols[i].name = "SlotSymbol: " + String.Format("{0:00}", i);
            symbols[i].SetLayer(symbolLayer);
        }
    }

    public virtual void InitSymbols()
    {
        for (int i = 0; i < slotTilesCount; i++)
        {
            int rand = GetRandom(0, SlotMN.GetSymbolCount());
            SymbolData symbolData = GameMN.Instance.gameData.symbols[rand];
            symbols[i].Setting(symbolData);
        }
    }
   
    public virtual void ReelMove(SpinValue spinValue ,Action rotCallBack, SymbolResult symbolResult = null)
    {
        // start spin delay
        spinStartDelay = Mathf.Max(0, spinStartDelay);
        float spinStartRandomizeF = Mathf.Clamp(spinStartRandomize / 100f, 0f, 0.2f);
        float startDelay = UnityEngine.Random.Range(spinStartDelay * (1.0f - spinStartRandomizeF), spinStartDelay * (1.0f + spinStartRandomizeF));

        spinValue.inRotTime = Mathf.Clamp(spinValue.inRotTime, 0, 1f);
        spinValue.inRotAngle = Mathf.Clamp(spinValue.inRotAngle, 0, 10);

        spinValue.outRotTime = Mathf.Clamp(spinValue.outRotTime, 0, 1f);
        spinValue.outRotAngle = Mathf.Clamp(spinValue.outRotAngle, 0, 10);

        float oldVal = 0f;
        tS = new TweenSeq();
        // tS.Add((callBack) => // in rotation part
        // {
        //     SimpleTween.Value(gameObject, 0f, spinValue.inRotAngle, spinValue.inRotTime)
        //                         .SetOnUpdate((float val) =>
        //                         {
        //                             TilesGroup.Rotate(val - oldVal, 0, 0);
        //                             oldVal = val;
        //                         })
        //                         .AddCompleteCallBack(() =>
        //                         {
        //                             callBack();
        //                         }).SetEase(spinValue.inRotType).SetDelay(startDelay);
        // });

        tS.Add((callBack) =>  // main rotation part
        {
            oldVal = 0f;
            spinSpeedMultiplier = Mathf.Max(0, spinSpeedMultiplier);

            bool isApplyResult = false;
            SetBlurIcon(true);
            SimpleTween.Value(gameObject, 0, spinValue.endRotated, spinValue.mainRotateTime + reelIndex * 0.1f)
                                .SetOnUpdate((float val) =>
                                {
                                // check rotation angle 
                                TilesGroup.Rotate(val - oldVal, 0, 0);
                                oldVal = val;
                                if(val < -45 && !isApplyResult)
                                {
                                    isApplyResult = true;
                                    ApplyLastResult();
                                }
                                })
                                .AddCompleteCallBack(() =>
                                {
                                    SetBlurIcon(false);
                                    ChangeAllSymbolWithoutResult();
                                    callBack();
                                    rotCallBack?.Invoke();
                                }).SetEase(spinValue.mainRotateType);
        });
        
        
        tS.Add((callBack) =>  // out rotation part
        {
            oldVal = 0f;
            SetBlurIcon(false);
            SimpleTween.Value(gameObject, 0, spinValue.outRotAngle, spinValue.outRotTime)
                                .SetOnUpdate((float val) =>
                                {
                                    TilesGroup.Rotate(val - oldVal, 0, 0);
                                    oldVal = val;
                                })
                                .AddCompleteCallBack(() =>
                                {
                                    rotCallBack?.Invoke();
                                    callBack();
                                }).SetEase(spinValue.outRotType);
        });

        tS.Start();
    }

    public virtual void ApplyLastResult()
    {
        int row = GameMN.Instance.gameData.row;
        for(int i = 0 ; i < row; i++)
        {
            int symbol = ResultMN.Instance.symbolResult.GetSymbol(reelIndex, i);
            SymbolData data = GameMN.Instance.gameData.symbols[symbol];
            symbols[i].Setting(data);
            symbols[i].SetBlurIcon(true);
        }
    }

    public virtual void ChangeAllSymbolWithoutResult()
    {
        int row = GameMN.Instance.gameData.row;
        for(int i = row ; i < symbols.Length; i++)
        {
            int rand = GetRandom(0, SlotMN.GetSymbolCount());
            SymbolData data = GameMN.Instance.gameData.symbols[rand];
            symbols[i].Setting(data);
            symbols[i].SetBlurIcon(true);
        }
    }

    public void SetBlurIcon(bool isBlur)
    {
        for(int i = 0; i < symbols.Length; i++){
            symbols[i].SetBlurIcon(isBlur);
        }
    }

    internal void CancelRotation()
    {
        SimpleTween.Cancel(gameObject, false);
        if (tS != null) tS.Break();
    }

    public int GetRandom(int min ,int max)
    {
        return UnityEngine.Random.Range(min, max);
    }
}