using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyCtrl : ShootableObjectCtrl
{
    [SerializeField] protected SpriteRenderer model;
    public SpriteRenderer Model => model;

    [SerializeField] protected Animator enemyAnimator;
    public Animator EnemyAnimator => enemyAnimator;

    [SerializeField] protected Rigidbody2D rb2d;
    public Rigidbody2D Rb2d => rb2d;

    protected override string GetObjectTypeString()
    {
        return ObjectType.Enemy.ToString();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyAnimator();
        this.loadRigidbody2D();
        this.LoadModel();
    }

    protected virtual void LoadEnemyAnimator()
    {
        if (this.enemyAnimator != null) return;
        this.enemyAnimator = transform.Find("Model").GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadEnemyAnimator", gameObject);
    }

    protected virtual void loadRigidbody2D()
    {
        if (this.rb2d != null) return;
        this.rb2d = transform.GetComponent<Rigidbody2D>();
        Debug.LogWarning(transform.name + ": loadRigidbody2D", gameObject);
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model").GetComponent<SpriteRenderer>();
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }
}
