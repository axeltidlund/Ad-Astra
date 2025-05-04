using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class GameLoop : StateMachine
{
    public UnityEvent<State, State> stateChangedEvent;
    public State intermissionState;
    public State activeState;
    public State endedState;

    public Damageable damageable;
    public Stats stats;

    public int selectionsLeft = 0;
    public bool ended = false;

    void Start()
    {
        Time.timeScale = 1f;
        intermissionState.Setup(this);
        activeState.Setup(this);
        endedState.Setup(this);
        
        state = activeState;
        SelectState();
        Debug.Log(state);
    }
    public void SelectState()
    {
        State _oldState = state;

        if (ended)
        {
            state = endedState;
        }
        else if (selectionsLeft > 0 && state != intermissionState) {
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
        damageable.health = Mathf.Min(stats.entityData.maxHealth, damageable.health + 5);
        selectionsLeft += 1;
    }

    public void HandleSelectInput(InputAction.CallbackContext context) {
        if (!context.started) return;
        if (selectionsLeft <= 0) return;
        if (state == intermissionState) return;
        SelectState();
    }

    public void PlayerHeatlh(float health)
    {
        if (health > 0) return;
        ended = true;
        SelectState();
    }
}
