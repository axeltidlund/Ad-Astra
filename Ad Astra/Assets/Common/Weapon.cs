using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    public bool canUse { get; protected set; }
    public bool isEquipped { get; protected set; }

    protected float _lastUse = 0;
    protected float _timeSinceLastUse => Time.fixedTime - _lastUse;

    public virtual void OnPress() { }
    public virtual void OnRelease() { }

    public virtual void EquipUpdate() {
        canUse = _timeSinceLastUse > (1 / weaponData.attackRate);
    }
    public virtual void OnEquip() {
        isEquipped = true;
    }
    public virtual void OnUnequip() {
        isEquipped = false;
    }
}
