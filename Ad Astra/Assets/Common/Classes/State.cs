using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    public bool isComplete { get; protected set; }

    protected float startTime;
    protected StateMachine input;
    public float time => Time.time - startTime;

    public virtual void Enter() { }
    public virtual void Do() { }
    public virtual void FixedDo() { }
    public virtual void Exit() { }

    public void Setup(StateMachine _stateMachine)
    {
        input = _stateMachine;
    }
}
