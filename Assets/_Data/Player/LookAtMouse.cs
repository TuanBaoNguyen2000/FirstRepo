using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MyMonoBehaviour
{
    [SerializeField] protected Vector3 mouseLookDirection;

    private void Update()
    {
        this.MouseRotation();
    }

    protected virtual void MouseRotation()
    {
        this.mouseLookDirection = InputManager.Instance.MousePos - transform.position;

        float angleToRotate = Mathf.Atan2(this.mouseLookDirection.y, this.mouseLookDirection.x) * Mathf.Rad2Deg;
        Quaternion qRotation = Quaternion.AngleAxis(angleToRotate, Vector3.forward);
        transform.rotation = qRotation;
    }
}
