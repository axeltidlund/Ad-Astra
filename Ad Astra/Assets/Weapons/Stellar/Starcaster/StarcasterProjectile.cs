using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarcasterProjectile : Projectile
{
    public override void Spawn(WeaponData weaponData, ProjectileData projectileData, Transform origin)
    {
        base.Spawn(weaponData, projectileData, origin);
        _rb.velocity = transform.right * _projectileData.travelSpeed * Time.fixedDeltaTime;
    }
    public override void AI()
    {
        _rb.velocity = _rb.velocity * .98f;
    }
}
