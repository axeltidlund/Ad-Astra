using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(Moveable))]
public class Damageable : MonoBehaviour
{
    public UnityEvent<float> onDamage;

    public Vector2 impulse;
    public float impulseTime = 0f;
    public float health { get; private set; }

    Stats stats;
    Moveable moveable;
    private void Awake()
    {
        stats = GetComponent<Stats>();
        moveable = GetComponent<Moveable>();
    }
    public void Setup(float maxHealth)
    {
        health = maxHealth;
    }

    public void Damage(float damage, Global.ReactiveType element, Vector2 _impulse, float _impulseDuration)
    {
        float finalDamage = damage * stats.GetGlobalResistance() * stats.GetResistance(System.Enum.GetName(typeof(Global.ReactiveType), element));
        Global.AugmentReactionTarget reaction = stats.ApplyElement(element);
        finalDamage = GeneralFunctions.instance.TriggerReaction(finalDamage, reaction, transform);

        health = Mathf.Max(0, health - finalDamage);
        Debug.Log(finalDamage);
        GeneralFunctions.instance.SpawnDamageIndicator(transform.position, finalDamage, finalDamage == damage ? .5f : 1f);
        onDamage.Invoke(health);

        if (_impulse == Vector2.zero || _impulseDuration <= 0) { return; }
        moveable.locked = true;
        impulse = _impulse;
        impulseTime = _impulseDuration;
    } 
    private void Update()
    {
        if (impulseTime > 0f)
        {
            impulseTime = Mathf.Max(0, impulseTime - Time.deltaTime);
            if (impulseTime <= 0f)
            {
                moveable.locked = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if (impulseTime > 0f)
        {
            moveable.Move(impulse.normalized, impulse.magnitude, true);
        }
    }
}
