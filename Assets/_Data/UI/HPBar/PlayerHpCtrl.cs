using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public class PlayerHpCtrl : MyMonoBehaviour
{
    [Header("HP Bar")]
    [SerializeField] protected PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl => playerCtrl;

    [SerializeField] protected PlayerHpSlider playerHpSlider;
    public PlayerHpSlider PlayerHpSlider => playerHpSlider;


    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
        this.LoadPlayerHpSlider();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        Debug.Log(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    protected virtual void LoadPlayerHpSlider()
    {
        if (this.playerHpSlider != null) return;
        this.playerHpSlider = transform.Find("Slider").GetComponent<PlayerHpSlider>();
        Debug.Log(transform.name + ": LoadPlayerHpSlider", gameObject);
    }

}
