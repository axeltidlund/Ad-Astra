using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voidstick : RangedWeapon
{
    private ProjectileShooter _shooter;
    private void Awake()
    {
        isEquipped = true;
        _shooter = GetComponent<ProjectileShooter>();
    }
    protected override void DoPress()
    {
        if (!canUse) return;

        for (int i = 0; i < 6; i++)
        {
            _shooter.FireProjectile(weaponData, firePoint.transform);
        }

        _lastUse = Time.fixedTime;
    }
    protected override void Reload()
    {
        
    }
}
