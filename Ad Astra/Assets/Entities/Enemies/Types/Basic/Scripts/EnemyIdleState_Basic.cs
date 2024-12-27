using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState_Basic : State
{
    public bool canSeePlayer = true;
    public override void Enter()
    {

    }
    public override void Do()
    {
        if (canSeePlayer) { isComplete = true; }
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
