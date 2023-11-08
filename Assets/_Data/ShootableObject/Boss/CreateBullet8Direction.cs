using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CreateBullet8Direction : BaseAbility
{
    [Header("Create Bullet 8 Direction")]
    [SerializeField] protected Vector3 direction;
    [SerializeField] protected Quaternion rotation;

    [SerializeField] protected float distance;
    [SerializeField] protected float distanCanShoot = 10f;
    [SerializeField] protected Vector3 targetPos;

    [Header("Create Bullet 8 Direction Animation")]
    [SerializeField] protected bool isCreatingBullet = false;
    [SerializeField] protected float createDelay = 0.5f;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.GetTargetPos();
        this.CreatingBullet();
    }

    protected virtual void GetTargetPos()
    {
        this.targetPos = this.abilitiesCtrl.BossCtrl.BossMovementAI.Target.position;
    }

    protected virtual void CreatingBullet()
    {
        if (!this.isReady) return;

        this.distance = Vector2.Distance(transform.position, targetPos);
        if (distance < this.distanCanShoot) return;

        this.PrepareCreatingBulletAnimation();

        Invoke(nameof(Create8Bullets), this.createDelay);
        
        this.Active();

    }

    protected virtual void Create8Bullets()
    {
        this.CreatingBulletFinish();

        for (int i = 0; i < 8; i++)
        {
            Vector3 spawnPos = this.CreateDirection(i);
            this.CreateRotation(i);
            Quaternion rotation = this.rotation;
            Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.enemyBullet, spawnPos, rotation);
            if (newBullet == null) return;
            newBullet.gameObject.SetActive(true);

            BulletCtrl bulletCtrl = newBullet.GetComponent<BulletCtrl>();
            bulletCtrl.SetShooter(transform.parent.parent);
        }

    }

    protected virtual Vector3 CreateDirection(int directionIndex)
    {
        switch (directionIndex)
        {
            case 0:
                this.direction = transform.position + new Vector3(1, 0, 0);
                break;
            case 1:
                this.direction = transform.position + new Vector3(1, 1, 0);
                break;
            case 2:
                this.direction = transform.position + new Vector3(0, 1, 0);
                break;
            case 3:
                this.direction = transform.position + new Vector3(-1, 1, 0);
                break;
            case 4:
                this.direction = transform.position + new Vector3(-1, 0, 0);
                break;
            case 5:
                this.direction = transform.position + new Vector3(-1, -1, 0);
                break;
            case 6:
                this.direction = transform.position + new Vector3(0, -1, 0);
                break;
            case 7:
                this.direction = transform.position + new Vector3(1, -1, 0);
                break;
        }
        return this.direction;
    }

    protected virtual void CreateRotation(int rotationIndex)
    {
        int angle = 2 * 180 * rotationIndex / 8;
        this.rotation = Quaternion.Euler(0,0,angle);
    }

    protected virtual void PrepareCreatingBulletAnimation()
    {
        this.isCreatingBullet = true;
        this.abilitiesCtrl.BossCtrl.EnemyAnimator.SetBool("isCreatingBullet", this.isCreatingBullet);
    }

    protected virtual void CreatingBulletFinish()
    {
        this.isCreatingBullet = false;
        this.abilitiesCtrl.BossCtrl.EnemyAnimator.SetBool("isCreatingBullet", this.isCreatingBullet);
    }
}
