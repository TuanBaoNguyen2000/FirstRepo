using UnityEngine;
using DG.Tweening;
using System.Collections;
public class Symbol : MonoBehaviour
{
    public SymbolData data;
    [SerializeField] private SpriteRenderer animSR;
    private SpriteRenderer sR;
    private bool isShowWin = false;
    private int animIndex = 0;
    [SerializeField] private GameObject border;
    private Color borderColor = Color.white;
    private void Awake() {
        sR = GetComponent<SpriteRenderer>();
    }
    
    private void Start()
    {
        SetBlurIcon(false);
    }

    private void Update() {
        if(!isShowWin || data.anims.Count == 0) return;
        ShowWinningSpriteAnim();
    }

    internal void Setting(SymbolData data)
    {
        this.data = data;
    }

    public void SetBlurIcon(bool blur)
    {
        ShowBorder(false);
        sR.sprite = blur ? data.blurSymbol : data.symbol;
        isShowWin = false;
        sR.enabled = true;
        animSR.gameObject.SetActive(false);
        
    }

    public void WinSetting(bool isShow, Color color)
    {
        this.isShowWin = isShow;
        this.borderColor = color;
        animIndex = 0;
        ShowBorder();
        if(data.anims.Count > 0) return;
            ShowWinningAnim(isShow);
    }

    private void ShowWinningSpriteAnim()
    {
        animSR.gameObject.SetActive(true);
        animSR.sprite = data.anims[animIndex];
        animIndex++;
        if(animIndex >= data.anims.Count) 
            animIndex = 0;
    }
    private void ShowWinningAnim(bool isShow)
    {
        animSR.gameObject.SetActive(isShow);
        sR.enabled =!isShow;
        animSR.sprite = data.symbol;

        if(!isShow)
            return;

        StartCoroutine(IEAnim());
    }

    IEnumerator IEAnim()
    {
        animSR.transform.DORotate(new Vector3(-360,0,0), 1, RotateMode.FastBeyond360).SetEase(Ease.InOutQuart);
        animSR.transform.DOScaleX(1.2f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        animSR.transform.DOScaleX(1f, 0.5f);
    }

    public void SetLayer(int layer){
        sR.sortingOrder = layer;
        animSR.sortingOrder = layer + 2;
    }

    public void ShowBorder(bool isShow = true)
    {
        border.SetActive(isShow);
        border.GetComponent<SpriteRenderer>().color = borderColor;
    }
}
