using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public State idleState;
    public State chaseState;
    //public State noControlState;

    public Moveable moveableComponent;

    public GameObject player;
    public EnemyData enemyData;

    void Start()
    {
        idleState.Setup(this);
        chaseState.Setup(this);
        //noControlState.Setup(this);

        state = idleState;
        player = FindAnyObjectByType<PlayerStateMachine>().gameObject;
    }
    void SelectState()
    {
        if ((idleState as EnemyIdleState_Basic).canSeePlayer)
        {
            state = chaseState;
        } else if (!(chaseState as EnemyChaseState_Basic).canSeePlayer)
        {
            state = idleState;
        }
        state.Enter();
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
