using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherstormProjectile : Projectile
{
    public override void Spawn(Data weaponData, ProjectileData projectileData, Transform origin)
    {
        base.Spawn(weaponData, projectileData, origin);
        _rb.velocity = _travelSpeed * Time.fixedDeltaTime * transform.right;
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
