using TMPro;
using UnityEngine;

public class TextScoreCtrl : MyMonoBehaviour
{
    [SerializeField] protected EnemySpawner enemySpawner;
    public EnemySpawner EnemySpawner => enemySpawner;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemySpawner();
    }

    protected virtual void LoadEnemySpawner()
    {
        if (this.enemySpawner != null) return;
        this.enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        Debug.LogWarning(transform.name + ": LoadEnemySpawner", gameObject);
    }

}
