using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Input))]
public class PlayerStateMachine : StateMachine
{
    public State idleState;
    public State movementState;
    //public State noControlState;

    public Moveable moveableComponent;

    public Vector2 movementInput;

    InputComponent inputComponent;

    public Rigidbody2D rb;
    public Animator animator;

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

        animator.SetFloat("Vertical", movementInput.y);
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Magnitude", movementInput.sqrMagnitude);
    }
    private void FixedUpdate()
    {
        state?.FixedDo();
    }
}
