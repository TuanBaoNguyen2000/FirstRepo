using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityRoll : BaseAbility
{
    [Header("Rolling Ability")]
    [SerializeField] protected Vector3 direction;
    [SerializeField] protected Vector3 targetPos;
    [SerializeField] protected float rollSpeed = 110000f;
    [SerializeField] protected Vector2 rollForce;
    [SerializeField] protected float distance;
    [SerializeField] protected float distanCanRoll = 10f;


    [Header("Rolling Ability Animation")]
    [SerializeField] protected bool isRolling = false;
    [SerializeField] protected float rollTimer = 1f;

    [SerializeField] protected bool isPrepareRolling = false;
    [SerializeField] protected float rollDelay = 1f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.GetTargetPos();
        this.Rolling();
    }

    protected virtual void GetTargetPos()
    {
        this.targetPos = this.abilitiesCtrl.BossCtrl.BossMovementAI.Target.position;
    }

    protected virtual void Rolling()
    {
        if (!this.isReady) return;

        this.distance = Vector2.Distance(transform.position, targetPos);
        if (distance > this.distanCanRoll) return;

        this.abilitiesCtrl.BossCtrl.Rb2d.velocity = Vector2.zero;
        this.PrepareRollingAnimation();

        Invoke(nameof(AddForceToRoll), this.rollDelay);
        
        this.Active();
    }

    protected virtual void AddForceToRoll()
    {
        this.PrepareRollingFinish();
        this.direction = (targetPos - transform.position).normalized;
        this.rollForce = direction * this.rollSpeed;
        this.abilitiesCtrl.BossCtrl.Rb2d.AddForce(this.rollForce);
        this.RollingAnimation();
    }

    protected virtual void RollingAnimation()
    {
        this.isRolling = true;
        Invoke(nameof(RollingFinish), this.rollTimer);
        this.abilitiesCtrl.BossCtrl.EnemyAnimator.SetBool("isRolling", this.isRolling);
    }

    protected virtual void RollingFinish()
    {
        //Debug.LogWarning("rolling finish");
        this.isRolling = false;
        this.abilitiesCtrl.BossCtrl.EnemyAnimator.SetBool("isRolling", this.isRolling);
    }

    protected virtual void PrepareRollingAnimation()
    {      
        this.isPrepareRolling = true;
        this.abilitiesCtrl.BossCtrl.EnemyAnimator.SetBool("isPrepareRolling", this.isPrepareRolling);
    }

    protected virtual void PrepareRollingFinish()
    {
        this.isPrepareRolling = false;
        this.abilitiesCtrl.BossCtrl.EnemyAnimator.SetBool("isPrepareRolling", this.isPrepareRolling);
    }


}
