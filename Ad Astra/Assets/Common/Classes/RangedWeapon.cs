using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public abstract class RangedWeapon : Weapon
{
    protected int magazineAmount;
    public int ammo { get; protected set; }
    public Transform firePoint;
    protected virtual void Reload() { // add delay
        ammo = magazineAmount;
    }
    protected override bool DoPress() {
        if (ammo <= 0) { Reload(); return false; }
        ammo -= 1;
        return true;
    }
    
    public void UpdateAmmo(int amount, int max) {
        magazineAmount = max;
        ammo = Mathf.Min(amount, max);
        Debug.Log(ammo);
    }
}
