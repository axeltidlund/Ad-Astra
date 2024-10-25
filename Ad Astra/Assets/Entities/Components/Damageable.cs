using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Damageable : MonoBehaviour
{
    public float health { get; private set; }
    Stats stats;
    private void Awake()
    {
        stats = GetComponent<Stats>();
    }
    public void Setup(float maxHealth)
    {
        health = maxHealth;
    }

    public void Damage(float damage, string element)
    {
        float finalDamage = damage * stats.GetGlobalResistance() * stats.GetResistance(element);
        health = Mathf.Max(0, health - finalDamage);
    }
}
