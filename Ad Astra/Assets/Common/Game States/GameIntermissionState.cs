using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIntermissionState : State
{
    public Animator uiAnimator;
    public override void Enter()
    {
        uiAnimator.SetBool("Selecting", true);
    }
    public override void Do()
    {
        
    }
    public override void Exit()
    {
        uiAnimator.SetBool("Selecting", false);
    }

    public void Click() {
        if (isComplete) return;
        isComplete = true;

        // Give 
    }
}
