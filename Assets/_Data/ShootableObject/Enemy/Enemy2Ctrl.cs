using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Ctrl : EnemyCtrl
{
    [SerializeField] protected Enemy2MovementAI enemy2MovementAI;
    public Enemy2MovementAI Enemy2MovementAI => enemy2MovementAI;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemy2MovementAI();
    }
    protected virtual void LoadEnemy2MovementAI()
    {
        if (this.enemy2MovementAI != null) return;
        this.enemy2MovementAI = transform.Find("EnemyMovement").GetComponent<Enemy2MovementAI>();
        Debug.LogWarning(transform.name + ": LoadEnemy2MovementAI", gameObject);
    }
}
