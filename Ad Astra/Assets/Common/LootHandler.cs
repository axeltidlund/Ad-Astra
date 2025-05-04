using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootHandler : MonoBehaviour
{
    public List<AugmentData> augmentLootTable;
    public List<WeaponData> weaponLootTable;

    public PlayerInventory playerInventory;

    public float pity = 0f;
    public AugmentData RollAugment()
    {
        float totalWeight = 0;
        AugmentData selected = default;

        foreach (AugmentData p in augmentLootTable)
        {
            int actualWeight = Global.rarityWeights[p.rarity];
            totalWeight += actualWeight;
        }

        float value = Random.value * totalWeight;

        foreach (AugmentData p in augmentLootTable)
        {
            int actualWeight = Global.rarityWeights[p.rarity];
            if (actualWeight >= value)
            {
                selected = p;
                break;
            }

            value -= actualWeight;
        }

        
        return selected;
    }
    public WeaponData RollWeapon()
    {
        float totalWeight = 0;
        WeaponData selected = default;

        foreach (WeaponData p in weaponLootTable)
        {
            int actualWeight = Global.rarityWeights[p.rarity];
            totalWeight += actualWeight;
        }

        float value = Random.value * totalWeight;

        foreach (WeaponData p in weaponLootTable)
        {
            int actualWeight = Global.rarityWeights[p.rarity];
            if (actualWeight >= value)
            {
                selected = p;
                break;
            }

            value -= actualWeight;
        }

        return selected;
    }

    public Data Roll() {
        float rand = Random.Range(0f, 1f);
        if (rand > (.3 + pity)) {
            pity += .05f;
            return RollAugment();
        } else {
            pity = 0f;
            return RollWeapon();
        }
    }
}
