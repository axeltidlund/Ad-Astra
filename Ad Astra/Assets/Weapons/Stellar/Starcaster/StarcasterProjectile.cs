using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class StarcasterProjectile : Projectile
{
    public GameObject explosion;
    public AudioClip clip;
    public override void Spawn(Data weaponData, ProjectileData projectileData, Transform origin)
    {
        base.Spawn(weaponData, projectileData, origin);
        _rb.velocity = _travelSpeed * Time.fixedDeltaTime * transform.right;
    }
    public override void AI()
    {

    }
    public override void OnHit()
    {
        if (!GeneralFunctions.instance.visualEffectsEnabled) { return; }
        GameObject go = Instantiate(explosion, transform.position, Quaternion.identity);
        go.GetComponent<Animator>().SetTrigger("Explode");
        go.GetComponentInChildren<VisualEffect>().SendEvent("Explode");
        GeneralFunctions.instance.PlaySound(clip, 1f, transform);
    }
}
