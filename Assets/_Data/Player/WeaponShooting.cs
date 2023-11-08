using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooting : MyMonoBehaviour
{
    [SerializeField] protected bool isShooting = false;
    [SerializeField] protected float shootDelay = 0.4f;
    [SerializeField] protected float shootTimer = 0f;

    [SerializeField] protected AudioCtrl audioCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAudioCtrl();
    }

    protected virtual void LoadAudioCtrl()
    {
        if (this.audioCtrl != null) return;
        this.audioCtrl = GameObject.Find("AudioManager").GetComponent<AudioCtrl>();
        Debug.Log(transform.name + ": LoadAudioCtrl", gameObject);
    }

    private void Update()
    {
        this.IsShooting();   
    }

    private void FixedUpdate()
    {
        this.Shooting();
    }

    protected virtual void Shooting()
    {
        if (!this.isShooting) return;

        this.shootTimer += Time.fixedDeltaTime;
        if (this.shootTimer < this.shootDelay) return;
        this.shootTimer = 0;

        this.audioCtrl.GetAudio("Fire").Play();

        Vector3 spawnPos = transform.position;
        Quaternion rotation = transform.parent.rotation;
        Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.bulletOne, spawnPos, rotation);
        if (newBullet == null) return;
        newBullet.gameObject.SetActive(true);    
        //Debug.Log("Shooting");
    }

    protected virtual bool IsShooting()
    {
        this.isShooting = InputManager.Instance.OnFiring == 1;
        return this.isShooting;
    }
}
