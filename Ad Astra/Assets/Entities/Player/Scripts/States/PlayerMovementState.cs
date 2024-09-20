using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : State
{
    public float moveSpeed = 15f;
    public override void Enter()
    {

    }
    public override void Do()
    {
        if (input.movementInput.magnitude == 0) { isComplete = true; }
    }
    public override void FixedDo()
    {
        input.moveableComponent.Move(input.movementInput, moveSpeed);
    }
    public override void Exit()
    {

    }
}
