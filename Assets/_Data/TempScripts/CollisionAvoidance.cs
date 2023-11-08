using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : MonoBehaviour
{
    public float speed = 5f;
    public float avoidanceDistance = 2f;
    public float raycastDistance = 1f;
    public LayerMask obstacleLayer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Tìm h??ng di chuy?n
        Vector2 direction = transform.right;

        // Ki?m tra xem có v?t c?n trong ph?m vi tránh
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, avoidanceDistance, obstacleLayer);

        if (hit.collider != null)
        {
            // N?u có v?t c?n, tìm h??ng tránh
            Vector2 avoidanceDirection = Vector2.Perpendicular(hit.normal).normalized;
            direction = avoidanceDirection;
        }

        // Di chuy?n theo h??ng ?ã tìm ???c
        rb.velocity = direction * speed;
    }
}
