using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActiveState : State
{
    private float spawnTimer = 0f;
    public float maxSpawnTime = 15f;
    public override void Enter()
    {

    }
    public override void Do()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= maxSpawnTime) {
            spawnTimer = 0f;
            // Spawn
        }
    }
    public override void Exit()
    {

    }
}
