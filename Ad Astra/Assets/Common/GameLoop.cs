using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameLoop : StateMachine
{
    public UnityEvent<State, State> stateChangedEvent;
    public State intermissionState;
    public State activeState;

    void Start()
    {
        intermissionState.Setup(this);
        activeState.Setup(this);
        
        state = activeState;
    }
    void SelectState()
    {
        State _oldState = state;

        state.Enter();
        stateChangedEvent.Invoke(state, _oldState);
    }
    void Update()
    {
        if (state.isComplete)
        {
            SelectState();
        }
        state.Do();
    }
    private void FixedUpdate()
    {
        state?.FixedDo();
    }
}
