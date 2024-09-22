using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string displayName;
    [TextArea(5, 10)]
    public string description;

    public float damage;
    public float attackRate;
    public float knockback;

    public bool isAutoUse;

    public GameObject gunPrefab;

    public Helpers.ReactiveType reactiveType;
}
