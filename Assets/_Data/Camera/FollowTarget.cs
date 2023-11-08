using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MyMonoBehaviour
{

    [SerializeField] protected Transform target;
    [SerializeField] protected float speed = 2f;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayer();
    }

    protected virtual void LoadPlayer()
    {
        if (this.target != null) return;
        this.target = GameObject.Find("Player").transform;
        Debug.LogWarning(transform.name + ": LoadPlayer", gameObject);
    }

    protected virtual void FixedUpdate()
    {
        this.Following();
    }

    protected virtual void Following()
    {
        if (this.target == null) return;
        transform.position = Vector3.Lerp(transform.position, this.target.position, Time.fixedDeltaTime * this.speed);
    }
}

