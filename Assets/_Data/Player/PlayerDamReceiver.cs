using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamReceiver : DamageReceiver
{
    //[SerializeField] protected ShootableObjectCtrl shootableObjectCtrl;
    protected int hpMaxPlayer = 5;

    [SerializeField] protected AudioCtrl audioCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        //this.LoadCtrl();
        this.LoadAudioCtrl();
    }

    protected virtual void LoadAudioCtrl()
    {
        if (this.audioCtrl != null) return;
        this.audioCtrl = GameObject.Find("AudioManager").GetComponent<AudioCtrl>();
        Debug.Log(transform.name + ": LoadAudioCtrl", gameObject);
    }

    protected override void OnDead()
    {
        this.audioCtrl.GetAudio("GameOver").Play();
        GameObject.Find("Player").SetActive(false);
        this.OnDeadFX();
        //this.OnDeadDrop();
        //this.shootableObjectCtrl.Despawn.DespawnObject();

    }

    protected virtual void OnDeadFX()
    {
        string fxName = this.GetOnDeadFXName();
        Transform fxOnDead = FXSpawner.Instance.Spawn(fxName, transform.position, transform.rotation);
        fxOnDead.gameObject.SetActive(true);
    }

    protected virtual string GetOnDeadFXName()
    {
        return FXSpawner.playerDeath;
    }

    public override void Reborn()
    {
        this.hpMax = this.hpMaxPlayer;
        base.Reborn();
    }
}
