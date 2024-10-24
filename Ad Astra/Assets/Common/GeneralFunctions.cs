using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralFunctions : MonoBehaviour
{
    public static GeneralFunctions instance;
    public GameObject player;
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
}
