using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BlackHole : Reaction
{
    public HitboxHandler hitboxHandler;
    public VisualEffect effect;
    public float hitRate = .25f;
    public int hitCount = 0;
    public float timer = 0f;
    public bool active = false;
    public override void Trigger(Transform position)
    {
        active = true;
        transform.position = position.position;
    }

    void DoHit() {
        List<RaycastHit2D> hits = hitboxHandler.Angular(360, transform, 2);
        reactionData.Shake();
        reactionData.Sound(transform);

        foreach (var enemy in hits)
        {
            Damageable damageable = enemy.collider.gameObject.GetComponent<Damageable>();
            Vector2 vec = (enemy.rigidbody.transform.position - transform.position).normalized;
            if (damageable == null) continue;
            damageable.Damage(reactionData.damage, reactionData.reactiveType, -vec * 2f, .25f, Global.AugmentReactionTarget.BlackHole);
        }

        if (hitCount >= (3 / hitRate)) {
            active = false;
            effect.Stop();
        }
    }
    void Update() {
        if (!active) return;
        timer += Time.deltaTime;
        if (timer > hitRate) {
            timer = 0f;
            hitCount += 1;
            DoHit();
        }
    }
}
