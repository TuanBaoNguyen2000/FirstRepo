using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEditor;

public class EnemyAI : MyMonoBehaviour
{
    [SerializeField] protected Transform target;
    public Transform Target => target;

    [SerializeField] protected float speed = 3000f;
    [SerializeField] protected float nextWaypointDistance = 3f;

    [SerializeField] protected Path path;
    [SerializeField] protected int currentWaypoint = 0;
    [SerializeField] protected bool reachedEndOfPath;

    [SerializeField] protected EnemyCtrl enemyCtrl;

    [SerializeField] protected Seeker seeker;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.loadTarget();
        this.LoadEnemyCtrl();
        this.loadSeeker();
    }

    protected virtual void loadTarget()
    {
        if (this.target != null) return;
        this.target = GameObject.Find("Player").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": loadTarget", gameObject);
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (this.enemyCtrl != null) return;
        this.enemyCtrl = transform.parent.GetComponent<EnemyCtrl>();
        Debug.LogWarning(transform.name + ": LoadEnemyCtrl", gameObject);
    }

    protected virtual void loadSeeker()
    {
        if (this.seeker != null) return;
        this.seeker = transform.GetComponent<Seeker>();
        Debug.LogWarning(transform.name + ": loadSeeker", gameObject);
    }

    protected override void Start()
    {
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    protected virtual void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(this.enemyCtrl.Rb2d.position, target.position, OnPathCompelete);
        }
    }

    protected virtual void OnPathCompelete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        this.EnemyMoving();
        this.EnemyAnimation();
    }

    protected virtual void EnemyAnimation()
    {        
        float enemyDirectionX = this.enemyCtrl.Rb2d.velocity.x;
        //this.enemyCtrl.EnemyAnimator.SetFloat("enemyDirection", enemyDirectionX);
        if (enemyDirectionX < 0)
        {
            this.enemyCtrl.Model.flipX = true;
        }
        else
        {
            this.enemyCtrl.Model.flipX = false;
        }
    }

    protected virtual void EnemyMoving()
    {
        if (path == null) return;   
        if (IsReachedEndOfPath()) return;

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - this.enemyCtrl.Rb2d.position).normalized;
        Vector2 force = direction * speed * Time.fixedDeltaTime;

        this.enemyCtrl.Rb2d.AddForce(force);

        float distance = Vector2.Distance(this.enemyCtrl.Rb2d.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance) currentWaypoint++;      
    }

    protected virtual bool IsReachedEndOfPath()
    {
        if (currentWaypoint >= path.vectorPath.Count) return true; 
        return false;
    }
}
