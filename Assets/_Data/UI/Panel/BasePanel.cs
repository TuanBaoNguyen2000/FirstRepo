using UnityEngine;
using UnityEngine.UI;

public abstract class BasePanel : MyMonoBehaviour
{
    [SerializeField] protected Image panel;

    protected override void Start()
    {
        base.Start();
        this.HidePanel();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPanel();
    }

    protected virtual void LoadPanel()
    {
        if (this.panel != null) return;
        this.panel = GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadPanel", gameObject);
    }

    public void ShowPanel()
    {
        this.panel.gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        this.panel.gameObject.SetActive(false);
    }

}
