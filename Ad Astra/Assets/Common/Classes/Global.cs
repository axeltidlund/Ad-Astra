using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
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
        Eclipse,
        Fusion,
        BlackHole,
        DarkMatter,
        Singularity,
        Fission,
        CosmicStorm,
        QuasarPulse,
        TimeDilation
    }

    public static Dictionary<ReactiveType, Dictionary<ReactiveType, AugmentReactionTarget>> reactionPaths = new Dictionary<ReactiveType, Dictionary<ReactiveType, AugmentReactionTarget>>()
    {
        { 
            ReactiveType.Quantum, new Dictionary<ReactiveType, AugmentReactionTarget>() 
            {
                { ReactiveType.Void, AugmentReactionTarget.Singularity },
                { ReactiveType.Gravity, AugmentReactionTarget.TimeDilation },
                { ReactiveType.Stellar, AugmentReactionTarget.QuasarPulse },
                { ReactiveType.Nebula, AugmentReactionTarget.Fission },
            } 
        },
        {
            ReactiveType.Nebula, new Dictionary<ReactiveType, AugmentReactionTarget>()
            {
                { ReactiveType.Void, AugmentReactionTarget.DarkMatter },
                { ReactiveType.Gravity, AugmentReactionTarget.CosmicStorm },
                { ReactiveType.Quantum, AugmentReactionTarget.Fission },
                { ReactiveType.Stellar, AugmentReactionTarget.None },
            }
        },
        {
            ReactiveType.Void, new Dictionary<ReactiveType, AugmentReactionTarget>()
            {
                { ReactiveType.Stellar, AugmentReactionTarget.Eclipse },
                { ReactiveType.Gravity, AugmentReactionTarget.BlackHole },
                { ReactiveType.Nebula, AugmentReactionTarget.DarkMatter },
                { ReactiveType.Quantum, AugmentReactionTarget.Singularity },
            }
        },
        {
            ReactiveType.Gravity, new Dictionary<ReactiveType, AugmentReactionTarget>()
            {
                { ReactiveType.Stellar, AugmentReactionTarget.Fusion },
                { ReactiveType.Void, AugmentReactionTarget.BlackHole },
                { ReactiveType.Nebula, AugmentReactionTarget.CosmicStorm },
                { ReactiveType.Quantum, AugmentReactionTarget.TimeDilation },
            }
        },
        {
            ReactiveType.Stellar, new Dictionary<ReactiveType, AugmentReactionTarget>()
            {
                { ReactiveType.Gravity, AugmentReactionTarget.Fusion },
                { ReactiveType.Void, AugmentReactionTarget.Eclipse },
                { ReactiveType.Quantum, AugmentReactionTarget.QuasarPulse },
                { ReactiveType.Nebula, AugmentReactionTarget.None },
            }
        },
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
        if (aura == trigger)
        {
            return AugmentReactionTarget.None;
        }
        return reactionPaths[aura][trigger];
    }
}
