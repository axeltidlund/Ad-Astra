using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlittingPhantasm : MeleeWeapon
{
    private HitboxHandler _hitbox;
    public Animator animator;
    private void Awake()
    {
        isEquipped = true;
        _hitbox = GetComponent<HitboxHandler>();
    }
    protected override void DoPress()
    {
        if (!canUse) return;
        _lastUse = Time.fixedTime;

        if (GeneralFunctions.instance.visualEffectsEnabled == true) {
            animator.SetTrigger("Swing");
        }

        List<RaycastHit2D> hits = _hitbox.Angular((int)(weaponData as MeleeWeaponData).coverageAngle, transform, (weaponData as MeleeWeaponData).radius);

        foreach (RaycastHit2D hit in hits)
        {
            Damageable damageable = hit.rigidbody.gameObject.GetComponent<Damageable>();
            if (damageable != null )
            {
                damageable.Damage(weaponData.damage, weaponData.reactiveType, transform.right.normalized * weaponData.knockbackStrength, weaponData.knockbackTime);
            }
        }
    }
}
