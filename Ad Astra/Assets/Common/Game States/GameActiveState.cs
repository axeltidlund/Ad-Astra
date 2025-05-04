using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActiveState : State
{
    public GameObject spawnTransforms;
    public GameObject enemyPrefab; // this needs to be changed
    private float spawnTimer = 0f;
    public float maxSpawnTime = 2f;

    float currentTime = 0f;

    float c = .025f;
    float d = 18.2f;
    public float currentDifficultyMultiplier;
    int lastLevel = 1;
    private void Awake()
    {
        currentDifficultyMultiplier = 1.5f;
    }
    public override void Enter()
    {
        Debug.Log("Game Loop is now Active.");
        UpdateLevel(lastLevel, 0);
    }
    public override void Do()
    {
        currentTime += Time.deltaTime;
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= maxSpawnTime * currentDifficultyMultiplier && GeneralFunctions.instance.enemyCount < 40) {
            spawnTimer = 0f;
            // Spawn

            int randomSpawnIndex = Random.Range(0, spawnTransforms.transform.childCount);
            Transform randomSpawnTransform = spawnTransforms.transform.GetChild(randomSpawnIndex);

            Instantiate(enemyPrefab, randomSpawnTransform.position, Quaternion.identity);
            GeneralFunctions.instance.enemyCount++;
        }
    }
    public override void Exit()
    {
            
    }

    public void UpdateLevel(int newLevel, int _)
    {
        lastLevel = newLevel;
        currentDifficultyMultiplier = (1f / (1f + Mathf.Exp(c * (newLevel - d))) + 0.2f) / 0.8f;
        GeneralFunctions.instance.globalDifficulty = currentDifficultyMultiplier;
    }
}
