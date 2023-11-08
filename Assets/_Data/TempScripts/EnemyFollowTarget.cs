using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowTarget : EnemyMovement
{
    [Header("Follow Target")]
    [SerializeField] protected Transform target;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.SetTarget();
    }

    protected override void FixedUpdate()
    {
        this.GetTargetPosition();
        base.FixedUpdate();
    }

    public virtual void SetTarget()
    {
        this.target = GameObject.Find("Player").transform;
    }

    protected virtual void GetTargetPosition()
    {
        this.targetPosition = this.target.position;
        this.targetPosition.z = 0;
    }
}