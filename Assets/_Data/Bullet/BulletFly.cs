using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MyMonoBehaviour
{
    [SerializeField] protected float bulletSpeed = 15f;
    [SerializeField] protected Vector3 bulletDirection = Vector3.right;

    private void Update()
    {
        transform.parent.Translate(this.bulletDirection * this.bulletSpeed * Time.deltaTime);
    }
}
