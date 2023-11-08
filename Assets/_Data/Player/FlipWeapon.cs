using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipWeapon : MyMonoBehaviour
{
    [SerializeField] protected Vector3 mouseLookDirection;
    [SerializeField] protected SpriteRenderer weaponRenderer;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpriteRenderer();
    }

    protected virtual void LoadSpriteRenderer()
    {
        if (this.weaponRenderer != null) return;
        this.weaponRenderer = transform.GetComponent<SpriteRenderer>();
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }

    private void Update()
    {
        this.FlipY();
    }

    protected virtual void FlipY()
    {
        this.mouseLookDirection = InputManager.Instance.MousePos - transform.position;

        if (this.mouseLookDirection.x < 0)
        {
            this.weaponRenderer.flipY = true;
        }
        else
        {
            this.weaponRenderer.flipY = false;
        }
    }
}
