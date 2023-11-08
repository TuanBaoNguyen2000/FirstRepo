using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MyMonoBehaviour
{
    [SerializeField] protected Vector3 targetDirection;

    [SerializeField] protected Enemy2Ctrl enemy2Ctrl;
    public Enemy2Ctrl Enemy2Ctrl => enemy2Ctrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemy2Ctrl();
    }

    protected virtual void LoadEnemy2Ctrl()
    {
        if (this.enemy2Ctrl != null) return;
        this.enemy2Ctrl = transform.parent.GetComponent<Enemy2Ctrl>();
        Debug.LogWarning(transform.name + ": LoadEnemy2Ctrl", gameObject);
    }

    private void Update()
    {
        this.TargetRotation();
    }

    protected virtual void TargetRotation()
    {
        this.targetDirection = enemy2Ctrl.Enemy2MovementAI.Target.position - transform.position;

        float angleToRotate = Mathf.Atan2(this.targetDirection.y, this.targetDirection.x) * Mathf.Rad2Deg;
        Quaternion qRotation = Quaternion.AngleAxis(angleToRotate, Vector3.forward);
        transform.rotation = qRotation;
    }
}
