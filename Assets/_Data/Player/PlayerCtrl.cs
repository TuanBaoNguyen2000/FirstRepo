using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCtrl : MyMonoBehaviour
{
    [SerializeField] protected Animator playerAnimator;
    public Animator PlayerAnimator => playerAnimator;

    [SerializeField] protected Rigidbody2D rb2d;
    public Rigidbody2D Rb2d => rb2d;

    [SerializeField] protected DamageReceiver damageReceiver;
    public DamageReceiver DamageReceiver => damageReceiver;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerAnimator();
        this.LoadRigidbody2D();
        this.LoadDamageReceiver();
    }    

    protected virtual void LoadPlayerAnimator()
    {
        if (this.playerAnimator != null) return;
        this.playerAnimator = transform.Find("Model").GetComponent<Animator>();
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void LoadRigidbody2D()
    {
        if (this.rb2d != null) return;
        this.rb2d = transform.GetComponent<Rigidbody2D>();
        Debug.Log(transform.name + ": LoadRigidbody2D", gameObject);
    }

    protected virtual void LoadDamageReceiver()
    {
        if (this.damageReceiver != null) return;
        this.damageReceiver = transform.Find("DamageReceiver").GetComponent<DamageReceiver>();
        Debug.Log(transform.name + ": LoadDamageReceiver", gameObject);
    }
}
