using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MyMonoBehaviour
{
    [SerializeField] protected float shootDelay = 3f;
    [SerializeField] protected float shootTimer = 0f;

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

    private void FixedUpdate()
    {
        this.EnemyShoot();
    }

    protected virtual void EnemyShoot()
    {
        if (!this.IsShooting()) return;

        this.shootTimer += Time.fixedDeltaTime;
        if (this.shootTimer < this.shootDelay) return;
        this.shootTimer = 0;

        //this.audioCtrl.GetAudio("Fire").Play();

        Vector3 spawnPos = transform.position;
        Quaternion rotation = transform.rotation;
        Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.enemyBullet, spawnPos, rotation);
        if (newBullet == null) return;
        newBullet.gameObject.SetActive(true);

        BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
        bulletCtrl.SetShooter(transform.parent.parent);
        //Debug.Log("Shooting");
    }

    protected virtual bool IsShooting()
    {
        return enemy2Ctrl.Enemy2MovementAI.Distance < enemy2Ctrl.Enemy2MovementAI.DistanceCanShoot;
    }
}
