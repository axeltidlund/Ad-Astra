using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidstickProjectile : Projectile
{
    public override void Spawn(Data weaponData, ProjectileData projectileData, Transform origin)
    {
        base.Spawn(weaponData, projectileData, origin);
        _rb.velocity = _travelSpeed * Time.fixedDeltaTime * transform.right;
    }
    public override void AI()
    {

    }
}
