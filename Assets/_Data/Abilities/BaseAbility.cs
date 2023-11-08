using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : MyMonoBehaviour
{
    [Header("Base Ability")]
    [SerializeField] protected AbilitiesCtrl abilitiesCtrl;
    public AbilitiesCtrl AbilitiesCtrl => abilitiesCtrl;

    [SerializeField] protected float timer = 0f;
    [SerializeField] protected float delay = 5f;
    [SerializeField] protected bool isReady = false;

    protected virtual void FixedUpdate()
    {
        this.Timing();
    }

    protected virtual void Update()
    {
        //
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.loadAbilitiesCtrl();
    }

    protected virtual void loadAbilitiesCtrl()
    {
        if (this.abilitiesCtrl != null) return;
        this.abilitiesCtrl = transform.parent.GetComponent<AbilitiesCtrl>();
        Debug.LogWarning(transform.name + ": loadAbilitiesCtrl", gameObject);
    }

    protected virtual void Timing()
    {
        if (this.isReady) return;
        this.timer += Time.fixedDeltaTime;
        if (this.timer < this.delay) return;
        this.isReady = true;
    }

    public virtual void Active()
    {
        this.isReady = false;
        this.timer = 0;
    }
}
