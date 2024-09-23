using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starcaster : RangedWeapon
{
    private ProjectileShooter _shooter;
    private void Awake()
    {
        _shooter = GetComponent<ProjectileShooter>();
    }
    public override void OnPress()
    {
        _shooter.FireProjectile(weaponData, firePoint.transform);
    }
    public override void Reload()
    {
        
    }
}
