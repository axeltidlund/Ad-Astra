using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public enum ReactiveType
    {
        Quantum,
        Stellar,
        Void,
        Gravity,
        Nebula
    }

    public static void FlipGameObject(GameObject go)
    {
        go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y * -1, go.transform.position.z);

        SpriteRenderer sprite = go.GetComponent<SpriteRenderer>();
        if (sprite == null) return;
        sprite.flipY = !sprite.flipY;
    }
}
