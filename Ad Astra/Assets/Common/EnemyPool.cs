using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool instance;

    public GameObject enemyPrefab;
    public int prewarmCount = 10;

    private readonly Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;

        for (int i = 0; i < prewarmCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform);
            enemy.SetActive(false);
            pool.Enqueue(enemy);
        }
    }

    public GameObject Request(Vector3 position)
    {
        GameObject enemy = pool.Count > 0 ? pool.Dequeue() : Instantiate(enemyPrefab, transform);

        enemy.transform.position = position;
        enemy.SetActive(true);
        return enemy;
    }

    public void Return(GameObject enemy)
    {
        enemy.SetActive(false);
        pool.Enqueue(enemy);
    }
}
