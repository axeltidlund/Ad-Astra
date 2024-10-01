using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidstickProjectile : Projectile
{
    public override void Spawn(WeaponData weaponData, ProjectileData projectileData, Transform origin)
    {
        base.Spawn(weaponData, projectileData, origin);
        _rb.velocity = transform.right * _projectileData.travelSpeed * Time.fixedDeltaTime;
    }
    public override void AI()
    {

    }
    public override void OnWall(Collider2D collision)
    {
        OnKill();
        this.gameObject.SetActive(false);
        Destroy(this.gameObject, 1f);
    }
}
