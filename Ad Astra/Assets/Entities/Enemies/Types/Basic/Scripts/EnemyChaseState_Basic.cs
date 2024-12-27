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

    EnemyStateMachine input_enemy;
    Vector2 direction;
    public override void Enter()
    {
        input_enemy = input as EnemyStateMachine;
        seeker = input_enemy.gameObject.GetComponent<Seeker>();
        aim = input_enemy.gameObject.GetComponentInChildren<EnemyAim>();

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
        if (distance < nextWaypointDistance) {
            currentWaypoint++;
        }
    }
    public override void FixedDo()
    {
        input_enemy.moveableComponent.Move(direction, input_enemy.enemyData.speed * Time.fixedDeltaTime, false); // calculate speed
    }
    public override void Exit()
    {

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
