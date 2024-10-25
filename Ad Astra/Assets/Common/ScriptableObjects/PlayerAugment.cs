using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAugmentData", menuName = "ScriptableObjects/Augments/PlayerAugmentData")]
public class PlayerAugmentData : StatAugmentData
{
    public Global.AugmentStat augmentStat;
    public Global.ReactiveType reactiveType;

    public override float Apply(float stat)
    {
        return isAdditive ? stat += amount: stat *= amount;
    }
}
