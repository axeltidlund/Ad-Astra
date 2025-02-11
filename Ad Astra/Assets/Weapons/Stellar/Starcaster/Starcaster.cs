using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starcaster : RangedWeapon
{
    private ProjectileShooter _shooter;
    private float charge = 0f;

    public GameObject chargeEffect;
    public GameObject chargeLight;
    private void Awake()
    {
        isEquipped = true;
        _shooter = GetComponent<ProjectileShooter>();
    }
    protected override bool DoPress()
    {
        if (ammo <= 0) { Reload(); return false; }
        if (!canUse) return false;

        if (charge < 1)
        {
            charge += Time.deltaTime;
            chargeEffect.SetActive(true);
            chargeLight.SetActive(true);
            return false;
        } else
        {
            _shooter.FireProjectile(weaponData, firePoint.transform);
            _lastUse = Time.fixedTime;
            playerAim.Recoil((weaponData as RangedWeaponData).recoil, 1f);
            charge = 0f;
            chargeEffect.SetActive(false);
            chargeLight.SetActive(false);
            ammo -= 1;
            return true;
        }
    }
    protected override void DoRelease()
    {
        charge = 0f;
        chargeEffect.SetActive(false);
        chargeLight.SetActive(false);
    }
}
