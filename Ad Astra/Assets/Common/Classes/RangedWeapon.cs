using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RangedWeapon : Weapon
{
    protected int magazineAmount;
    public int ammo { get; protected set; }
    public Transform firePoint;
    protected virtual void Reload() { }
    
    public void UpdateAmmo(int amount, int max) {
        magazineAmount = max;
        ammo = Mathf.Min(amount, max);
        Debug.Log(ammo);
    }
}
