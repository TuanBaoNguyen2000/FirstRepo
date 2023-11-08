using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScore : BaseText
{
    [SerializeField] protected TextScoreCtrl textScoreCtrl;

    [SerializeField] protected int score;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTextScoreCtrl();
    }

    protected virtual void LoadTextScoreCtrl()
    {
        if (this.textScoreCtrl != null) return;
        this.textScoreCtrl = transform.parent.GetComponent<TextScoreCtrl>();
        Debug.LogWarning(transform.name + ": LoadTextScoreCtrl", gameObject);
    }

    protected virtual void FixedUpdate()
    {
        this.UpdateScore();
    }

    protected virtual void UpdateScore()
    {
        score = this.textScoreCtrl.EnemySpawner.EnemyDeathCount;

        this.text.SetText("Score: " +  score);
    }
}
