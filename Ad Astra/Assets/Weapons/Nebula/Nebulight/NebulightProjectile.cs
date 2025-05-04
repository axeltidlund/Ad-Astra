using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class NebulightProjectile : Projectile
{
    float offset = 0;
    public override void Spawn(Data weaponData, ProjectileData projectileData, Transform origin)
    {
        base.Spawn(weaponData, projectileData, origin);
        _rb.velocity = _travelSpeed * Time.fixedDeltaTime * transform.right;
        offset = Random.value;
    }
    public override void AI()
    {
        _rb.velocity += (Vector2)transform.up * Mathf.Cos(_timeLeft * Mathf.PI * 5 + offset * Mathf.PI) * .1f;
        _rb.velocity *= Mathf.Pow(.98f, Time.deltaTime * 60);
    }
}
