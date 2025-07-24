using System.Collections.Generic;
using UnityEngine;

// Pool for enemy prefabs with weighted random spawning
public class EnemyPool : MonoBehaviour
{
    public static EnemyPool instance;

    [System.Serializable]
    public class WeightedEnemy
    {
        public GameObject prefab;
        public float weight = 1f;

        [HideInInspector]
        public Queue<GameObject> pool = new Queue<GameObject>();
    }

    public List<WeightedEnemy> enemies = new List<WeightedEnemy>();
    public int prewarmCount = 10;

    private void Awake()
    {
        instance = this;

        for (int i = 0; i < enemies.Count; i++)
        {
            var entry = enemies[i];
            entry.pool = new Queue<GameObject>();
            for (int j = 0; j < prewarmCount; j++)
            {
                entry.pool.Enqueue(CreateEnemy(entry.prefab, i));
            }
        }
    }

    private GameObject CreateEnemy(GameObject prefab, int index)
    {
        GameObject enemy = Instantiate(prefab, transform);
        enemy.SetActive(false);
        var marker = enemy.GetComponent<PooledEnemy>();
        if (marker == null) marker = enemy.AddComponent<PooledEnemy>();
        marker.poolIndex = index;
        return enemy;
    }

    private int GetRandomIndex()
    {
        float totalWeight = 0f;
        foreach (var e in enemies) totalWeight += e.weight;
        if (totalWeight <= 0f) return 0;
        float r = Random.value * totalWeight;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (r <= enemies[i].weight) return i;
            r -= enemies[i].weight;
        }
        return enemies.Count - 1;
    }

    public GameObject Request(Vector3 position)
    {
        int index = GetRandomIndex();
        var entry = enemies[index];
        GameObject enemy = entry.pool.Count > 0 ? entry.pool.Dequeue() : CreateEnemy(entry.prefab, index);

        enemy.transform.position = position;
        enemy.SetActive(true);
        return enemy;
    }

    public void Return(GameObject enemy)
    {
        var marker = enemy.GetComponent<PooledEnemy>();
        if (marker == null || marker.poolIndex < 0 || marker.poolIndex >= enemies.Count)
        {
            Destroy(enemy);
            return;
        }

        enemy.SetActive(false);
        enemies[marker.poolIndex].pool.Enqueue(enemy);
    }
}
