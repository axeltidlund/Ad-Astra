using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyChaseState_Basic : State
{
    public bool canSeePlayer = false;

    EnemyStateMachine input_enemy;
    Vector2 direction;
    public override void Enter()
    {
        input_enemy = input as EnemyStateMachine;
    }
    public override void Do()
    {
        if (!canSeePlayer) { isComplete = true; }
        direction = (input_enemy.player.transform.position - this.transform.position).normalized;
    }
    public override void FixedDo()
    {
        input_enemy.moveableComponent.Move(direction, input_enemy.enemyData.speed * Time.fixedDeltaTime); // calculate speed
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
