using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletImpact : BulletImpact
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Obstacle") base.OnTriggerEnter2D(other);
        return;
    }
}
