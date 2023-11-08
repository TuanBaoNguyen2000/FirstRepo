using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy2MovementAI : EnemyAI
{
    [Header("Enemy 2 AI")]
    [SerializeField] protected float distance;
    public float Distance => distance;

    [SerializeField] protected float distanceCanShoot;
    public float DistanceCanShoot => distanceCanShoot;

    [SerializeField] protected float enemy2Distance = 10f;

    protected override void EnemyMoving()
    {
        if (path == null) return;
        if (IsReachedEndOfPath()) return;

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - this.enemyCtrl.Rb2d.position).normalized;
        Vector2 force = direction * speed * Time.fixedDeltaTime;

        this.enemyCtrl.Rb2d.AddForce(force);

        float distance = Vector2.Distance(this.enemyCtrl.Rb2d.position, path.vectorPath[currentWaypoint]);
        this.distance = distance;
        this.distanceCanShoot = this.enemy2Distance;
        if (distance < nextWaypointDistance + this.enemy2Distance) currentWaypoint++;
    }
}
