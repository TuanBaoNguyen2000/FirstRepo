using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerRandom : MyMonoBehaviour
{
    [Header("Spawner Random")]
    [SerializeField] protected SpawnerCtrl spawnerCtrl;
    [SerializeField] protected float randomDelay = 1f;
    [SerializeField] protected float randomTimer = 0f;
    [SerializeField] protected float randomLimit = 10f;

    protected Vector3 pos;
    protected Quaternion rot;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (this.spawnerCtrl != null) return;
        this.spawnerCtrl = GetComponent<SpawnerCtrl>();
        Debug.LogWarning(transform.name + ": LoadJunkCtrl", gameObject);
    }

    protected virtual void FixedUpdate()
    {
        this.EnemySpawning();
    }

    protected virtual void EnemySpawning()
    {
        //if (this.IsBossAlive()) return;
        if (this.RandomReachLimit()) return;

        this.randomTimer += Time.fixedDeltaTime;
        if (this.randomTimer < this.randomDelay) return;
        this.randomTimer = 0;

        Transform ranPoint = this.spawnerCtrl.SpawnPoints.GetRandom();
        this.pos = ranPoint.position;
        this.rot = transform.rotation;

        this.XSignFX(pos, rot);

        Invoke(nameof(GetEnemyToSpawn), 1f);

    }

    protected virtual void GetEnemyToSpawn()
    {
        Transform prefab = this.spawnerCtrl.Spawner.RandomPrefab();
        Transform obj = this.spawnerCtrl.Spawner.Spawn(prefab, this.pos, this.rot);
        obj.gameObject.SetActive(true);

    }

    protected virtual bool RandomReachLimit()
    {
        int currentEnemy = this.spawnerCtrl.Spawner.SpawnedCount;
        return currentEnemy >= this.randomLimit;
    }

    //protected virtual bool IsBossAlive()
    //{
    //    if (EnemySpawner.Instance.Holder.Find("Boss") == null) return false;
    //    return true;
    //}

    protected virtual void XSignFX(Vector3 pos, Quaternion rot)
    {
        string fxName = this.GetXSignFXName();
        Transform fxOnDead = FXSpawner.Instance.Spawn(fxName, pos, rot);
        fxOnDead.gameObject.SetActive(true);
    }

    protected virtual string GetXSignFXName()
    {
        return FXSpawner.xSign;
    }
}