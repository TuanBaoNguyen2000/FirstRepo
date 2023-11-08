using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManagerCtrl : MyMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl => playerCtrl;

    [SerializeField] protected GameOverPanel gameOverPanel;
    public GameOverPanel GameOverPanel => gameOverPanel;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
        this.LoadGameOverPanel();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        Debug.Log(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    protected virtual void LoadGameOverPanel()
    {
        if (this.gameOverPanel != null) return;
        this.gameOverPanel = transform.Find("GameOverPanel").GetComponent<GameOverPanel>();
        Debug.Log(transform.name + ": LoadGameOverPanel", gameObject);
    }
}
