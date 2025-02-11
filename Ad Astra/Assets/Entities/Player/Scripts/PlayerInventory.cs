using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    public WeaponData[] weapons = new WeaponData[3];
    public List<AugmentData> augmentations;

    private int _currentWeaponIndex = 0;
    private GameObject _currentWeaponPrefab;
    private Dictionary<string, int> _ammo = new Dictionary<string, int>();

    public Transform weaponHolder;
    public Transform aim;

    public delegate void SwitchDelegate(Weapon weapon);
    public SwitchDelegate switchWeapon;

    private void Awake()
    {
        SpawnWeapon(_currentWeaponIndex);
    }
    public void SwitchWeapon(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        int i = Convert.ToInt32(context.control.name) - 1;
        if (_currentWeaponIndex == i) return;

        SpawnWeapon(i);
        Weapon weaponHandler = _currentWeaponPrefab.GetComponent<Weapon>();
        switchWeapon?.Invoke(weaponHandler);
    }
    private void SpawnWeapon(int i)
    {
        if (_currentWeaponPrefab != null)
        {
            RangedWeapon previousWeaponHandler = _currentWeaponPrefab.GetComponent<RangedWeapon>();
            if (previousWeaponHandler != null) {
                _ammo[weapons[_currentWeaponIndex].displayName] = previousWeaponHandler.ammo;
            }
            Destroy(_currentWeaponPrefab);
        }
        _currentWeaponIndex = i;
        if (weapons[_currentWeaponIndex] == null) return;
        _currentWeaponPrefab = Instantiate(weapons[_currentWeaponIndex].gunPrefab, weaponHolder);

        RangedWeapon weaponHandler = _currentWeaponPrefab.GetComponent<RangedWeapon>();
        if (weaponHandler == null) { return; }

        RangedWeaponData weaponData = (RangedWeaponData)weapons[_currentWeaponIndex];
        string weaponName = weaponData.displayName;
        int currentAmmo = _ammo.ContainsKey(weaponName) ? _ammo[weaponName] : weaponData.magazineAmount;
        weaponHandler.UpdateAmmo(currentAmmo, weaponData.magazineAmount);
    }

    public void HandleWeaponPress(bool started)
    {
        if (_currentWeaponPrefab == null) return;
        if (started)
        {
            _currentWeaponPrefab.GetComponent<Weapon>()?.OnPress();
        } else
        {
            _currentWeaponPrefab.GetComponent<Weapon>()?.OnRelease();
        }
    }
}
