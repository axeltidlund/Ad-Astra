using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameLoop : StateMachine
{
    public UnityEvent<State, State> stateChangedEvent;
    public State intermissionState;
    public State activeState;

    int selectionsLeft = 0;

    void Start()
    {
        intermissionState.Setup(this);
        activeState.Setup(this);
        
        state = activeState;
        SelectState();
        Debug.Log(state);
    }
    public void SelectState()
    {
        State _oldState = state;

        if (selectionsLeft > 0) {
            state = intermissionState;
            selectionsLeft -= 1;
        } else {
            state = activeState;
        }
        _oldState.Exit();
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

    public void HandleLevelUp(int newLevel, int oldLevel) {
        if (newLevel % 5 != 0) return;
        selectionsLeft += 1;
        SelectState();
    }
}
