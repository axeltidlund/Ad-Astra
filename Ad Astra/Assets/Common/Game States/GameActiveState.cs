using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActiveState : State
{
    public GameObject spawnTransforms;
    public GameObject enemyPrefab; // this needs to be changed
    private float spawnTimer = 0f;
    public float maxSpawnTime = 2f;
    public override void Enter()
    {
        Debug.Log("Game Loop is now Active.");
    }
    public override void Do()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= maxSpawnTime) {
            spawnTimer = 0f;
            // Spawn

            int randomSpawnIndex = Random.Range(0, spawnTransforms.transform.childCount);
            Transform randomSpawnTransform = spawnTransforms.transform.GetChild(randomSpawnIndex);

            Instantiate(enemyPrefab, randomSpawnTransform.position, Quaternion.identity);
        }
    }
    public override void Exit()
    {
        
    }
}
