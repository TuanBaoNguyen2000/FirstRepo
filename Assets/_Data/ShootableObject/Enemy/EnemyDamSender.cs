using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamSender : DamageSender
{
    [SerializeField] protected CircleCollider2D circleCollider;
    public CircleCollider2D CircleCollider => circleCollider;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCircleCollider();
    }

    protected virtual void LoadCircleCollider()
    {
        if (this.circleCollider != null) return;
        this.circleCollider = transform.GetComponent<CircleCollider2D>();
        Debug.LogWarning(transform.name + ": LoadCircleCollider", gameObject);
    }
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("bbbbbbbbbbbbbbbb");
        if (collision.gameObject.tag == "Player")
        {
            this.Send(collision.transform);
        }
    }

}
