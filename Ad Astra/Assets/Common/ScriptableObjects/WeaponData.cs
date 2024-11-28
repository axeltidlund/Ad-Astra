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

    public float knockbackStrength;
    public float knockbackTime;

    public bool isAutoUse;

    public GameObject gunPrefab;

    public Global.ReactiveType reactiveType;
    public Global.Rarities rarity;

    public float shakeDuration;
    public float shakeSpeed;
    public float shakeAmp;

    public AudioClip shootSound;

    public void Shake(bool canUse)
    {
        if (!canUse) { return; }
        GeneralFunctions.instance.ShakeCamera(shakeDuration, shakeAmp, shakeSpeed);
    }
    public void Sound(bool canUse, Transform origin)
    {
        if (!canUse) { return; }
        GeneralFunctions.instance.PlaySound(shootSound, 1f, origin);
    }
}
