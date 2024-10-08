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
        All,
        Eclipse,
        Fusion,
        BlackHole,
        DarkMatter,
        Singularity,
        Fission,
        CosmicStorm,
        QuasarPulse
    }
    public static void FlipGameObject(GameObject go)
    {
        go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y * -1, go.transform.position.z);

        SpriteRenderer sprite = go.GetComponent<SpriteRenderer>();
        if (sprite == null) return;
        sprite.flipY = !sprite.flipY;
    }
}
