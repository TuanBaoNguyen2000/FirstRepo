using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableObjectDamReceiver : DamageReceiver
{
    [Header("Shootable Object")]
    [SerializeField] protected ShootableObjectCtrl shootableObjectCtrl;

    [SerializeField] protected AudioCtrl audioCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
        this.LoadAudioCtrl();
    }

    protected virtual void LoadCtrl()
    {
        if (this.shootableObjectCtrl != null) return;
        this.shootableObjectCtrl = transform.parent.GetComponent<ShootableObjectCtrl>();
        Debug.Log(transform.name + ": LoadCtrl", gameObject);
    }

    protected virtual void LoadAudioCtrl()
    {
        if (this.audioCtrl != null) return;
        this.audioCtrl = GameObject.Find("AudioManager").GetComponent<AudioCtrl>();
        Debug.Log(transform.name + ": LoadAudioCtrl", gameObject);
    }

    protected override void OnDead()
    {
        this.audioCtrl.GetAudio("EnemyDeath").Play();
        this.OnDeadFX();
        //this.OnDeadDrop();
        this.shootableObjectCtrl.Despawn.DespawnObject();

    }

    //protected virtual void OnDeadDrop()
    //{
    //    Vector3 dropPos = transform.position;
    //    Quaternion dropRot = transform.rotation;
    //    ItemDropSpawner.Instance.Drop(this.shootableObjectCtrl.ShootableObject.dropList, dropPos, dropRot);
    //}

    protected virtual void OnDeadFX()
    {
        string fxName = this.GetOnDeadFXName();
        Transform fxOnDead = FXSpawner.Instance.Spawn(fxName, transform.position, transform.rotation);
        fxOnDead.gameObject.SetActive(true);
    }

    protected virtual string GetOnDeadFXName()
    {
        return FXSpawner.enemy1Death;
    }

    public override void Reborn()
    {
        this.hpMax = this.shootableObjectCtrl.ShootableObject.hpMax;
        base.Reborn();
    }
}