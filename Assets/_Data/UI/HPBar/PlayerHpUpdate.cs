using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpUpdate : MyMonoBehaviour
{
    [SerializeField] protected PlayerHpCtrl playerHpCtrl;


    protected virtual void FixedUpdate()
    {
        this.HPBarUpdate();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerHpCtrl();
    }

    protected virtual void LoadPlayerHpCtrl()
    {
        if (this.playerHpCtrl != null) return;
        this.playerHpCtrl = transform.GetComponent<PlayerHpCtrl>();
        Debug.Log(transform.name + ": LoadPlayerHpCtrl", gameObject);
    }

    protected virtual void HPBarUpdate()
    {
        float hp = this.playerHpCtrl.PlayerCtrl.DamageReceiver.HP;
        float maxHp = this.playerHpCtrl.PlayerCtrl.DamageReceiver.HPMax;

        this.playerHpCtrl.PlayerHpSlider.SetCurrentHP(hp);
        this.playerHpCtrl.PlayerHpSlider.SetMaxHP(maxHp);
    }
}
