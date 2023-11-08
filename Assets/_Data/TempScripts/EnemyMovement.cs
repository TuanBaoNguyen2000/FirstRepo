using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MyMonoBehaviour
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    [SerializeField] protected Vector3 targetPosition;
    [SerializeField] protected float speed = 0.05f;
    [SerializeField] protected float distance = 1f;
    [SerializeField] protected float minDistance = 1f;

    [SerializeField] protected Vector3 enemyDirection;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }

    protected virtual void FixedUpdate()
    {
        this.EnemyAnimation();
        this.Moving();
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (this.enemyCtrl != null) return;
        this.enemyCtrl = transform.parent.GetComponent<EnemyCtrl>();
        Debug.LogWarning(transform.name + ": LoadEnemyCtrl", gameObject);
    }

    protected virtual void EnemyAnimation()
    {
        this.enemyDirection = this.targetPosition - transform.position;
        float enemyDirectionX = enemyDirection.x;
        this.enemyCtrl.EnemyAnimator.SetFloat("enemyDirection", enemyDirectionX);
    }

    protected virtual void Moving()
    {
        this.distance = Vector3.Distance(transform.position, this.targetPosition);
        if (this.distance < this.minDistance) return;

        //Vector3 newPos = Vector3.Lerp(transform.parent.position, targetPosition, this.speed);
        Vector3 newPos = Vector3.MoveTowards(transform.parent.position, targetPosition, this.speed);
        transform.parent.position = newPos;
    }
}