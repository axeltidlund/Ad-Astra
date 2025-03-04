using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singularity : Reaction
{
    public HitboxHandler hitboxHandler;
    public Animator animator;
    public override void Trigger(Transform position)
    {
        List<RaycastHit2D> hits = hitboxHandler.Angular(360, position, 2);
        reactionData.Shake();
        reactionData.Sound(position);
        animator.SetTrigger("play");

        foreach (var enemy in hits)
        {
            Damageable damageable = enemy.collider.gameObject.GetComponent<Damageable>();
            if (damageable == null) continue;
            damageable.Damage(reactionData.damage, reactionData.reactiveType, Vector2.zero, 0f, Global.AugmentReactionTarget.Singularity);
        }
    }
}
