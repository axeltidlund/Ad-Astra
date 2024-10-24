using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState_Basic : State
{
    public override void Enter()
    {

    }
    public override void Do()
    {
        if ((input as EnemyStateMachine).player != null) { isComplete = true; }
    }
    public override void Exit()
    {

    }
}
