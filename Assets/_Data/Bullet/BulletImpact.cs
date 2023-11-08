using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BulletImpact : BulletAbstract
{
    [Header("Bullet Impart")]
    [SerializeField] protected CircleCollider2D circleCollider2D;
    [SerializeField] protected Rigidbody2D _rigidbody;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
        this.LoadRigibody();
    }

    protected virtual void LoadCollider()
    {
        if (this.circleCollider2D != null) return;
        this.circleCollider2D = GetComponent<CircleCollider2D>();
        this.circleCollider2D.isTrigger = true;
        this.circleCollider2D.radius = 0.15f;
        Debug.Log(transform.name + ": LoadCollider", gameObject);
    }

    protected virtual void LoadRigibody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = GetComponent<Rigidbody2D>();
        this._rigidbody.isKinematic = true;
        Debug.Log(transform.name + ": LoadRigibody", gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet") return;

        this.bulletCtrl.BulletDespawn.DespawnObject();
        this.bulletCtrl.DamageSender.Send(other.transform);
        this.CreateImpactFX();
    }

    protected virtual void CreateImpactFX()
    {
        string fxname = this.GetFXExplosion();

        Vector3 hitPos = transform.position;
        Quaternion hitRot = transform.rotation;
        Transform fxExplosion = FXSpawner.Instance.Spawn(fxname, hitPos, hitRot);
        fxExplosion.gameObject.SetActive(true);
    }

    protected virtual string GetFXExplosion()
    {
        return FXSpawner.explosion1;
    }
}