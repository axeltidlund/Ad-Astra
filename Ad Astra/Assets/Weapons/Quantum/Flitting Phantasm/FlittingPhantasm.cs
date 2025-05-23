using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlittingPhantasm : MeleeWeapon
{
    private HitboxHandler _hitbox;
    public Animator animator;
    public GameObject effect;
    public Transform slashPoint;
    public int flip = -1;
    private void Awake()
    {
        isEquipped = true;
        _hitbox = GetComponent<HitboxHandler>();
    }
    protected override bool DoPress()
    {
        if (!canUse) return false;
        flip *= -1;
        transform.localScale = new Vector3(flip, 1, 1);
        _lastUse = Time.fixedTime;

        if (GeneralFunctions.instance.visualEffectsEnabled == true) {
            animator.SetTrigger("Swing");

            GameObject go = Instantiate(effect);
            go.transform.SetPositionAndRotation(slashPoint.position, playerAim.aimTransform.rotation);
            go.transform.localScale = new Vector3(1, -flip, 1);
        }

        List<RaycastHit2D> hits = _hitbox.Angular((int)(weaponData as MeleeWeaponData).coverageAngle, transform, (weaponData as MeleeWeaponData).radius);

        foreach (RaycastHit2D hit in hits)
        {
            Damageable damageable = hit.rigidbody.gameObject.GetComponent<Damageable>();
            if (damageable != null )
            {
                damageable.Damage(weaponData.damage, weaponData.reactiveType, transform.right.normalized * weaponData.knockbackStrength, weaponData.knockbackTime);
            }
        }
        return true;
    }
}
