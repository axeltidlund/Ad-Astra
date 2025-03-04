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

    public void Damage(float damage, Global.ReactiveType element, Vector2 _impulse, float _impulseDuration, Global.AugmentReactionTarget oldReaction = Global.AugmentReactionTarget.None)
    {
        bool isPlayer = gameObject.name != "Player";
        Global.AugmentReactionTarget reaction = Global.AugmentReactionTarget.None;

        float finalDamage = damage * stats.GetGlobalResistance() * stats.GetResistance(System.Enum.GetName(typeof(Global.ReactiveType), element));
        if (oldReaction == Global.AugmentReactionTarget.None) {
            reaction = stats.ApplyElement(element);
            finalDamage = GeneralFunctions.instance.TriggerReaction(finalDamage, reaction, transform);
        }

        if (isPlayer)
        {
            if (GeneralFunctions.instance.PlayerAugmentCount("Bet") > 0)
            {
                finalDamage *= Random.Range(0f, 2f);
            }
            if (GeneralFunctions.instance.PlayerAugmentCount("Gamble") > 0)
            {
                float rand = Random.Range(0f, 1f);
                if (rand >= .5)
                {
                    finalDamage *= 2f;
                }
                else if (rand <= .3)
                {
                    finalDamage *= 0f;
                }
            }

            List<StatAugmentData> multiplicatives = new List<StatAugmentData>();
            List<StatAugmentData> additives = new List<StatAugmentData>();

            foreach (AugmentData augment in GeneralFunctions.instance.GetAugmentDatas())
            {
                if (augment is ReactionAugmentData)
                {
                    ReactionAugmentData data = augment as ReactionAugmentData;
                    if (oldReaction == Global.AugmentReactionTarget.None) continue;
                    if (data.targetReaction != oldReaction) continue;
                    if (data.isAdditive)
                    {
                        additives.Add(data);
                    } else
                    {
                        multiplicatives.Add(data);
                    }

                } else if (augment is PlayerAugmentData)
                {
                    PlayerAugmentData data = augment as PlayerAugmentData;
                    if (data.augmentStat != Global.AugmentStat.DMG) continue;
                    if (data.isAdditive)
                    {
                        additives.Add(data);
                    }
                    else
                    {
                        multiplicatives.Add(data);
                    }
                }
            }

            foreach(StatAugmentData multiplicativeAugment in multiplicatives)
            {
                finalDamage = multiplicativeAugment.Apply(finalDamage);
            }
            foreach (StatAugmentData additiveAugment in additives)
            {
                finalDamage = additiveAugment.Apply(finalDamage);
            }
        }
        health = Mathf.Max(0, health - finalDamage);
        GeneralFunctions.instance.SpawnDamageIndicator(transform.position, Mathf.Round(finalDamage * 10) / 10, finalDamage == damage ? .5f : 1f);
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
