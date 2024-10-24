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
    }
    void SelectState()
    {
        if (player)
        {
            state = chaseState;
        } else
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (!collision.gameObject.tag.Equals("Player")) return;
        player = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.tag.Equals("Player")) return;
        player = null;
    }
}
