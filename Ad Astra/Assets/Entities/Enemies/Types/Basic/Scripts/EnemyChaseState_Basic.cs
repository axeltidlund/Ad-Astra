using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyChaseState_Basic : State
{
    public bool canSeePlayer = false;

    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    EnemyAim aim;
    public HitboxHandler hitboxHandler;
    public EnemyData enemyData;

    float attackTimer = 0f;

    EnemyStateMachine input_enemy;
    Vector2 direction;
    public override void Enter()
    {
        input_enemy = input as EnemyStateMachine;
        seeker = input_enemy.gameObject.GetComponent<Seeker>();
        aim = input_enemy.gameObject.GetComponentInChildren<EnemyAim>();

        if (!IsInvoking("UpdatePath"))
            InvokeRepeating("UpdatePath", 0f, .2f);
    }
    void UpdatePath() {
        seeker.StartPath(transform.position, input_enemy.player.transform.position, OnPathComplete);
    }
    void OnPathComplete(Path p) {
        if (!p.error) {
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

        if (currentWaypoint >= path.vectorPath.Count) {
            reachedEndOfPath = true;
            return;
        } else {
            reachedEndOfPath = false;
        }

        direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;

        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
        float playerDistance = Vector2.Distance(transform.position, input_enemy.player.transform.position);
        if (distance < nextWaypointDistance) {
            currentWaypoint++;
        }
        if (attackTimer <= 0)
        {
            if (playerDistance >= nextWaypointDistance) return;

            List<RaycastHit2D> hits = hitboxHandler.Angular(360, transform, .8f, true);
            foreach (RaycastHit2D hit in hits)
            {
                Damageable damageable = hit.collider.gameObject.GetComponent<Damageable>();
                if (damageable == null) continue;
                damageable.Damage(enemyData.attack, Global.ReactiveType.None, Vector2.zero, 0f);
                attackTimer = 1f;
            }
        }
    }
    public override void FixedDo()
    {
        input_enemy.moveableComponent.Move(direction, input_enemy.enemyData.speed * Time.fixedDeltaTime, false); // calculate speed
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
