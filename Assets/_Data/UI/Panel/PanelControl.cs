using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControl : MyMonoBehaviour
{
    [SerializeField] protected PanelManagerCtrl panelManagerCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPanelManagerCtrl();
    }

    protected virtual void FixedUpdate()
    {
        this.ControlGameOverPanel();
    }

    protected virtual void LoadPanelManagerCtrl()
    {
        if (this.panelManagerCtrl != null) return;
        this.panelManagerCtrl = transform.GetComponent<PanelManagerCtrl>();
        Debug.Log(transform.name + ": LoadPanelManagerCtrl", gameObject);
    }

    protected virtual void ControlGameOverPanel()
    {
        float hp = this.panelManagerCtrl.PlayerCtrl.DamageReceiver.HP;
        if (hp != 0) this.panelManagerCtrl.GameOverPanel.HidePanel();
        else this.panelManagerCtrl.GameOverPanel.ShowPanel();
    }
}
