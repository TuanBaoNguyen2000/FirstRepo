using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpSlider : BaseSlider
{
    [Header("Player HP")]
    [SerializeField] protected float currentHP = 1;
    [SerializeField] protected float maxHP = 1;

    protected virtual void FixedUpdate()
    {
        this.HPShowing();
    }

    protected virtual void HPShowing()
    {
        float hpPercent = this.currentHP / this.maxHP;
        this.slider.value = hpPercent;
    }

    protected override void OnChanged(float newValue)
    {
        //Debug.LogWarning("newVlaue"+ newValue);

    }

    public virtual void SetMaxHP(float maxHP)
    {
        this.maxHP = maxHP;
    }

    public virtual void SetCurrentHP(float currentHP)
    {
        this.currentHP = currentHP;
    }

}
