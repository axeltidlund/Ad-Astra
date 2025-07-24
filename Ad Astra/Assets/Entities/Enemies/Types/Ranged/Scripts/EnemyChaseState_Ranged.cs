using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyChaseState_Ranged : State
{
    public bool canSeePlayer = false;

    public float nextWaypointDistance = 3f;
    public float maintainDistance = 7f;
    public float fleeDistance = 5f;

    Path path;
    int currentWaypoint = 0;

    Seeker seeker;
    EnemyAim aim;
    ProjectileShooter shooter;
    public WeaponData weaponData;
    public EnemyData enemyData;

    float attackTimer = 0f;

    EnemyStateMachine input_enemy;
    Vector2 direction;

    public override void Enter()
    {
        input_enemy = input as EnemyStateMachine;
        seeker = input_enemy.gameObject.GetComponent<Seeker>();
        aim = input_enemy.gameObject.GetComponentInChildren<EnemyAim>();
        shooter = input_enemy.gameObject.GetComponent<ProjectileShooter>();

        InvokeRepeating("UpdatePath", 0f, .3f);
    }

    void UpdatePath()
    {
        if (seeker == null) return;
        seeker.StartPath(transform.position, input_enemy.player.transform.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public override void Do()
    {
        attackTimer -= Time.deltaTime;
        aim.UpdateAim(input_enemy.player.transform.position);
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        float playerDistance = Vector2.Distance(transform.position, input_enemy.player.transform.position);
        Vector2 toPlayer = ((Vector2)input_enemy.player.transform.position - (Vector2)transform.position).normalized;

        if (playerDistance > maintainDistance)
        {
            direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
        }
        else if (playerDistance < fleeDistance)
        {
            direction = -toPlayer;
        }
        else
        {
            direction = Vector2.zero;
        }

        if (canSeePlayer && attackTimer <= 0f && playerDistance <= enemyData.detectionRange)
        {
            shooter.FireProjectile(weaponData, aim.aimTransform);
            attackTimer = 1f / enemyData.attackSpeed;
        }
    }

    public override void FixedDo()
    {
        input_enemy.moveableComponent.Move(direction, input_enemy.enemyData.speed * Time.fixedDeltaTime, false);
    }

    public override void Exit()
    {
        CancelInvoke("UpdatePath");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.tag.Equals("Player")) return;
        canSeePlayer = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.tag.Equals("Player")) return;
        canSeePlayer = false;
    }
}
