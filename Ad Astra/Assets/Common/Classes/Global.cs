using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    public static float diminishRate = .8f;
    public enum ReactiveType
    {
        None,
        Quantum,
        Stellar,
        Void,
        Gravity,
        Nebula
    }
    public enum Rarities
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    public enum AugmentStat
    {
        HP,
        SPD,
        USE_SPD,
        RELOAD_SPD,
        ATK,
        DMG
    }
    public enum AugmentReactionTarget
    {
        None,
        All,
        Fusion,
        BlackHole,
        Singularity,
        Fission,
        CosmicStorm,
        QuasarPulse,
        Supernova
    }

    public static Dictionary<ReactiveType, Dictionary<ReactiveType, AugmentReactionTarget>> reactionPaths = new Dictionary<ReactiveType, Dictionary<ReactiveType, AugmentReactionTarget>>()
    {
        { 
            ReactiveType.Quantum, new Dictionary<ReactiveType, AugmentReactionTarget>() 
            {
                { ReactiveType.Void, AugmentReactionTarget.None },
                { ReactiveType.Gravity, AugmentReactionTarget.Singularity },
                { ReactiveType.Stellar, AugmentReactionTarget.QuasarPulse },
                { ReactiveType.Nebula, AugmentReactionTarget.Fission },
            } 
        },
        {
            ReactiveType.Nebula, new Dictionary<ReactiveType, AugmentReactionTarget>()
            {
                { ReactiveType.Void, AugmentReactionTarget.None },
                { ReactiveType.Gravity, AugmentReactionTarget.CosmicStorm },
                { ReactiveType.Quantum, AugmentReactionTarget.Fission },
                { ReactiveType.Stellar, AugmentReactionTarget.Supernova },
            }
        },
        {
            ReactiveType.Void, new Dictionary<ReactiveType, AugmentReactionTarget>()
            {
                { ReactiveType.Stellar, AugmentReactionTarget.None },
                { ReactiveType.Gravity, AugmentReactionTarget.BlackHole },
                { ReactiveType.Nebula, AugmentReactionTarget.None },
                { ReactiveType.Quantum, AugmentReactionTarget.None },
            }
        },
        {
            ReactiveType.Gravity, new Dictionary<ReactiveType, AugmentReactionTarget>()
            {
                { ReactiveType.Stellar, AugmentReactionTarget.Fusion },
                { ReactiveType.Void, AugmentReactionTarget.BlackHole },
                { ReactiveType.Nebula, AugmentReactionTarget.CosmicStorm },
                { ReactiveType.Quantum, AugmentReactionTarget.Singularity },
            }
        },
        {
            ReactiveType.Stellar, new Dictionary<ReactiveType, AugmentReactionTarget>()
            {
                { ReactiveType.Gravity, AugmentReactionTarget.Fusion },
                { ReactiveType.Void, AugmentReactionTarget.None },
                { ReactiveType.Quantum, AugmentReactionTarget.QuasarPulse },
                { ReactiveType.Nebula, AugmentReactionTarget.Supernova },
            }
        },
    };

    public static Dictionary<Rarities, int> rarityWeights = new Dictionary<Rarities, int>
    {
        { Rarities.Common, 50 },
        { Rarities.Rare, 30 },
        { Rarities.Epic, 15 },
        { Rarities.Legendary, 5 }
    };
    public static void FlipGameObject(GameObject go)
    {
        go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y * -1, go.transform.position.z);

        SpriteRenderer sprite = go.GetComponent<SpriteRenderer>();
        if (sprite == null) return;
        sprite.flipY = !sprite.flipY;
    }

    public static AugmentReactionTarget GetReaction(ReactiveType aura, ReactiveType trigger)
    {
        if (aura == trigger || aura == ReactiveType.None)
        {
            return AugmentReactionTarget.None;
        }
        return reactionPaths[aura][trigger];
    }

    public static bool IsAugmentForCorrectStat(AugmentStat playerStat, AugmentStat augmentStat)
    {
        return playerStat == augmentStat;
    }
}
