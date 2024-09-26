using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReactionAugmentData", menuName = "ScriptableObjects/Augments/ReactionAugmentData")]
public class ReactionAugmentData : StatAugmentData
{
    public Global.AugmentReactionTarget targetReaction;
}
