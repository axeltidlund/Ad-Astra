using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starcaster : RangedWeapon
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
        _shooter.FireProjectile(weaponData, firePoint.transform);
        _lastUse = Time.fixedTime;
        playerAim.Recoil((weaponData as RangedWeaponData).recoil, 1f);
    }
    protected override void Reload()
    {
        
    }
}
