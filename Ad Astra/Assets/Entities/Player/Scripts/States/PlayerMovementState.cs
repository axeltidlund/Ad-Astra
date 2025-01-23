using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : State
{
    public float moveSpeed = 15f;
    public SpriteRenderer spriteRenderer;
    public override void Enter()
    {

    }
    public override void Do()
    {
        if ((input as PlayerStateMachine).movementInput.magnitude == 0) { isComplete = true; }
        if ((input as PlayerStateMachine).movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    public override void FixedDo()
    {
        (input as PlayerStateMachine).moveableComponent.Move((input as PlayerStateMachine).movementInput, moveSpeed * Time.fixedDeltaTime, false);
    }
    public override void Exit()
    {

    }
}
