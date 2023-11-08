using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : EnemyCtrl
{
    [SerializeField] protected BossMovementAI bossMovementAI;
    public BossMovementAI BossMovementAI => bossMovementAI;

    protected override string GetObjectTypeString()
    {
        return ObjectType.Boss.ToString();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBossMovementAI();
    }

    protected virtual void LoadBossMovementAI()
    {
        if (this.bossMovementAI != null) return;
        this.bossMovementAI = transform.GetComponentInChildren<BossMovementAI>();
        Debug.LogWarning(transform.name + ": LoadBossMovementAI", gameObject);
    }
}
