using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesCtrl : MyMonoBehaviour
{
    [SerializeField] protected BossCtrl bossCtrl;
    public BossCtrl BossCtrl => bossCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.loadBossCtrl();
    }

    protected virtual void loadBossCtrl()
    {
        if (this.bossCtrl != null) return;
        this.bossCtrl = transform.parent.GetComponent<BossCtrl>();
        Debug.LogWarning(transform.name + ": loadBossCtrl", gameObject);
    }
}
