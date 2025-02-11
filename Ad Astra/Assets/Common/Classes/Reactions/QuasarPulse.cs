using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuasarPulse : Reaction
{
    public HitboxHandler hitboxHandler;
    public Animator animator;
    public override void Trigger(Transform position)
    {
        List<RaycastHit2D> list1 = hitboxHandler.Rect(0, transform, 1, 100);
        List<RaycastHit2D> list2 = hitboxHandler.Rect(0, transform, 1, -100);
        reactionData.Shake();
        reactionData.Sound(position);
        animator.SetTrigger("play");

        List<RaycastHit2D> list3 = list1.Union<RaycastHit2D>(list2).ToList();
        foreach (var enemy in list3)
        {
            Damageable damageable = enemy.collider.gameObject.GetComponent<Damageable>();
            if (damageable == null) return;
            damageable.Damage(reactionData.damage, reactionData.reactiveType, Vector2.zero, 0f, true);
        }
    }
}
