using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance => instance;


    [SerializeField] protected Vector3 mousePos;
    public Vector3 MousePos => mousePos;

    [SerializeField] protected float moveX;
    public float MoveX => moveX;
    [SerializeField] protected float moveY;
    public float MoveY => moveY;


    [SerializeField] protected float onFiring;
    public float OnFiring => onFiring;

    private void Awake()
    {
        if (InputManager.instance != null) Debug.LogError("Only 1 InputManager allow to exist");
        InputManager.instance = this;
    }

    private void Update()
    {
        this.GetDirection();
        this.GetMouseDown();
    }

    private void FixedUpdate()
    {
        this.GetMousePos();
    }

    protected virtual void GetMousePos()
    {
        this.mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.mousePos.z = 0;
    }

    protected virtual void GetDirection()
    {
        this.moveX = Input.GetAxisRaw("Horizontal");
        this.moveY = Input.GetAxisRaw("Vertical");
    }

    protected virtual void GetMouseDown()
    {
        this.onFiring = Input.GetAxis("Fire1");
    }

}
