using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionAvoidance : EnemyFollowTarget
{
    //public float speed = 5f;
    public float avoidanceDistance = 2f;
    public float raycastDistance = 1f;
    public LayerMask obstacleLayer;

    public Rigidbody2D rb;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody();
    }

    protected virtual void LoadRigidbody()
    {
        if (this.rb != null) return;
        this.rb = transform.GetComponent<Rigidbody2D>();
        Debug.LogWarning(transform.name + ": LoadRigidbody", gameObject);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Moving()
    {
        this.distance = Vector3.Distance(transform.position, this.targetPosition);
        if (this.distance < this.minDistance) return;

        Vector2 direction = transform.parent.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.parent.position, direction, avoidanceDistance, obstacleLayer);
        if (hit.collider != null)
        {
            Vector2 avoidanceDirection = Vector2.Perpendicular(hit.normal).normalized;
            direction = avoidanceDirection;
        }
        rb.velocity = direction * speed;

        Vector3 newPos = Vector3.MoveTowards(transform.parent.position, targetPosition, this.speed);
        transform.parent.position = newPos;
    }
}
