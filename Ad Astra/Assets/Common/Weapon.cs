using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public WeaponData weaponData;
    public bool canUse { get; protected set; } = false;
    public bool isEquipped { get; protected set; }

    protected float _lastUse = 0;
    protected float _timeSinceLastUse => Time.fixedTime - _lastUse;

    private bool _isKeyHeld = false;

    public void OnPress() {
        _isKeyHeld = true;
        DoPress();
    }
    public void OnRelease() {
        _isKeyHeld = false;
        DoRelease();
    }

    protected virtual void DoPress() { }
    protected virtual void DoRelease() { }

    public void Update() {
        if (!isEquipped) return;
        canUse = _timeSinceLastUse > (1 / weaponData.attackRate);

        if (weaponData.isAutoUse && _isKeyHeld)
        {
            DoPress();
        }
    }
    public virtual void OnEquip() {
        isEquipped = true;
    }
    public virtual void OnUnequip() {
        isEquipped = false;
    }
}
