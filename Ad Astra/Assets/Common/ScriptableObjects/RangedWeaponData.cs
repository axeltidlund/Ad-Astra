using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData/RangedWeaponData")]
public class RangedWeaponData : WeaponData
{
    public int magazineAmount;
    public float reloadTime;

    public float recoil;
    public float shotRange;
    public float spread;
}
