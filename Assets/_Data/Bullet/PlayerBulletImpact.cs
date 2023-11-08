using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletImpact : BulletImpact
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") return;
        base.OnTriggerEnter2D(other);
    }
}
