using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralFunctions : MonoBehaviour
{
    public static GeneralFunctions instance;
    public GameObject player;
    public GameObject indicator;
    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public bool PlayerHasAugment(string augmentName)
    {
        bool hasAugment = false;

        if (player.GetComponent<PlayerInventory>().augmentations.Count == 0) return false;
        foreach (AugmentData augment in player.GetComponent<PlayerInventory>().augmentations)
        {
            if (augment.name.Equals(augmentName))
            {
                hasAugment = true;
            }
        }

        return hasAugment;
    }

    public void SpawnDamageIndicator(Vector2 position, float damage, float duration)
    {
        GameObject go = Instantiate(indicator);
        go.transform.position = position + new Vector2(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f));
        
        DamageIndicator di = go.GetComponent<DamageIndicator>();
        di.Setup(damage.ToString(), duration);
    }
}
