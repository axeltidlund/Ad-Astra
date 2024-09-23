using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarcasterProjectile : Projectile
{
    public override void AI()
    {
        _rb.velocity = transform.right * _projectileData.travelSpeed * Time.fixedDeltaTime;
    }
}
