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

    void Awake()
    {
        idleState.Setup(this);
        chaseState.Setup(this);
        //noControlState.Setup(this);

        state = idleState;
        player = FindAnyObjectByType<PlayerStateMachine>().gameObject;
    }

    private void OnEnable()
    {
        Damageable damageable = GetComponent<Damageable>();
        damageable.Setup(enemyData.maxHealth / GeneralFunctions.instance.globalDifficulty);
        state = idleState;
    }
    void SelectState()
    {
        if ((idleState as EnemyIdleState_Basic).canSeePlayer)
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
    
    public void OnDamage(float newHealth) {
        if (newHealth <= 0) {
            GeneralFunctions.instance.RewardXP(enemyData.xp);
            GeneralFunctions.instance.enemyCount--;
            EnemyPool.instance.Return(gameObject);
        }
    }
}
