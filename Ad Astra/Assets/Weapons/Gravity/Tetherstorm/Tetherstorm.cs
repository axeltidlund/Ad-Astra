using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetherstorm : RangedWeapon
{
    private ProjectileShooter _shooter;
    private void Awake()
    {
        isEquipped = true;
        _shooter = GetComponent<ProjectileShooter>();
    }
    protected override bool DoPress()
    {
        if (!canUse) return false;
        if (base.DoPress() == false) return false;
        _shooter.FireProjectile(weaponData, firePoint.transform);
        _lastUse = Time.fixedTime;
        playerAim.Recoil((weaponData as RangedWeaponData).recoil, .2f);
        return true;
    }
}
