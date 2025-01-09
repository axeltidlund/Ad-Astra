using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherstormProjectile : Projectile
{
    public override void Spawn(WeaponData weaponData, ProjectileData projectileData, Transform origin)
    {
        base.Spawn(weaponData, projectileData, origin);
        _rb.velocity = transform.right * _projectileData.travelSpeed * Time.fixedDeltaTime;
        GeneralFunctions.instance.tetherstormBullets.Add(_rb);
    }
    public override void AI()
    {
        if (_rb == null) {
            Destroy(gameObject);
            return;
        }
        GeneralFunctions.instance.AttractTetherbullet(_rb);
    }

    public override void OnHit()
    {
        GeneralFunctions.instance.tetherstormBullets.Remove(_rb);
    }
}
