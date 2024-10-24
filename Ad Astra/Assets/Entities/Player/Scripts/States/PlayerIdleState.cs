using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State
{
    public override void Enter()
    {

    }
    public override void Do()
    {
        if ((input as PlayerStateMachine).movementInput.magnitude > 0) { isComplete = true; }
    }
    public override void Exit()
    {

    }
}
