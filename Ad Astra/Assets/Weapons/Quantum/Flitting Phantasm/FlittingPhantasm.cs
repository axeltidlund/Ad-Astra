using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlittingPhantasm : MeleeWeapon
{
    private HitboxHandler _hitbox;
    private void Awake()
    {
        isEquipped = true;
        _hitbox = GetComponent<HitboxHandler>();
    }
    protected override void DoPress()
    {
        if (!canUse) return;

        List<RaycastHit2D> hits = _hitbox.Angular((int)(weaponData as MeleeWeaponData).coverageAngle, transform, (weaponData as MeleeWeaponData).radius);
        Debug.Log(hits.Count);

        _lastUse = Time.fixedTime;
    }
}
