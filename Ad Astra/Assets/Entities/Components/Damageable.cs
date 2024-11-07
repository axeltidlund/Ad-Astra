using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Stats))]
public class Damageable : MonoBehaviour
{
    public UnityEvent onDamage;
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

    public void Damage(float damage, Global.ReactiveType element)
    {
        onDamage.Invoke();
        float finalDamage = damage * stats.GetGlobalResistance() * stats.GetResistance(System.Enum.GetName(typeof(Global.ReactiveType), element));
        health = Mathf.Max(0, health - finalDamage);
        Debug.Log(finalDamage);
        Debug.Log(element);
    }
}
