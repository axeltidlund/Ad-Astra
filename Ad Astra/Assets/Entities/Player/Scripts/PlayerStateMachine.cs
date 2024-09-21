using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Input))]
public class PlayerStateMachine : MonoBehaviour
{
    public State idleState;
    public State movementState;
    //public State noControlState;

    public Moveable moveableComponent;

    State state;

    public Vector2 movementInput;

    InputComponent inputComponent;

    void Start()
    {
        idleState.Setup(this);
        movementState.Setup(this);
        //noControlState.Setup(this);

        state = idleState;
        inputComponent = GetComponent<InputComponent>();
    }
    void SelectState()
    {
        if (movementInput.magnitude > 0)
        {
            state = movementState;
        }
        else
        {
            state = idleState;
        }
        state.Enter();
    }
    void Update()
    {
        movementInput = inputComponent.movementInput;
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
