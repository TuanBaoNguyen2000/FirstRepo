using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MyMonoBehaviour
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected Vector2 direction;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
    }
    

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = transform.parent.GetComponent<PlayerCtrl>();
        Debug.Log(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    private void FixedUpdate()
    {
        this.Moving();
    }
    protected virtual void Moving()
    {
        float horizontal = InputManager.Instance.MoveX;
        float vertical = InputManager.Instance.MoveY;

        //get input for animator ================
        this.playerCtrl.PlayerAnimator.SetFloat("horizontal", horizontal);
            //this.animator.SetFloat("vertical", vertical);
        if (horizontal != 0 || vertical != 0)
        {
            this.playerCtrl.PlayerAnimator.SetBool("isMoving", true);
        }else
        {
            this.playerCtrl.PlayerAnimator.SetBool("isMoving", false);

        }
        //get input done =================

        Vector2 inputVector = new Vector2(horizontal, vertical);

        this.direction = inputVector.normalized * moveSpeed;
        this.playerCtrl.Rb2d.MovePosition(this.playerCtrl.Rb2d.position + this.direction * Time.fixedDeltaTime);
      
    }
}
