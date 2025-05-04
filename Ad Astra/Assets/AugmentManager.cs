using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentManager : MonoBehaviour
{
    public PlayerMovementState playerMovementState;
    public PlayerData playerData;
    public PlayerInventory playerInventory;

    public float momentumShiftCooldown = 0f;
    public float momentumShiftTimeLeft = 0f;

    public bool adaptiveStrikes = false;
    public int deiCount = 0;

    public float useSpeed = 1f;

    private void Start()
    {
        playerInventory.switchWeapon += SwitchWeapon;
    }

    public float AdaptiveStrikesValue()
    {
        if (GeneralFunctions.instance.PlayerAugmentCount("Adaptive Strikes") == 0) return 1f;
        if (adaptiveStrikes)
        {
            return 1.2f * GeneralFunctions.instance.StackValue(GeneralFunctions.instance.PlayerAugmentCount("Adaptive Strikes"));
        } else {
            return 1f;
        }
    }

    public float DEIValue()
    {
        if (GeneralFunctions.instance.PlayerAugmentCount("DEI") == 0) return 1f;
        return Mathf.Exp(-0.15f * deiCount) * 2;
    }
    private void Update()
    {
        momentumShiftCooldown -= Time.deltaTime;
        momentumShiftTimeLeft -= Time.deltaTime;

        float totalMovementChange = 1f;
        if (momentumShiftTimeLeft > 0f && GeneralFunctions.instance.PlayerAugmentCount("Momentum Shift") > 0)
        {
            totalMovementChange *= 1.2f;
        }

        playerMovementState.moveSpeed = playerData.speed * totalMovementChange;

        float useSpeed = 1f;
        if (GeneralFunctions.instance.PlayerAugmentCount("Quick Trigger") > 0)
        {
            useSpeed *= 1.15f * GeneralFunctions.instance.StackValue(GeneralFunctions.instance.PlayerAugmentCount("Quick Trigger"));
        }
    }

    public float ApplyAugments(float damage)
    {
        return damage * AdaptiveStrikesValue() * DEIValue();
    }

    public void SwitchWeapon(Weapon weapon)
    {
        deiCount = 0;
        adaptiveStrikes = true;
        if (momentumShiftCooldown <= 0f)
        {
            momentumShiftCooldown = 10f;
            momentumShiftTimeLeft = 3f;
        }

        weapon.GetComponent<Weapon>().fireWeapon += Attack;
    }

    public void Attack(bool canUse)
    {
        if (!canUse) return;
    }

    public void TakeDamage()
    {

    }

    public void EnemyHit()
    {
        if (GeneralFunctions.instance.PlayerAugmentCount("DEI") > 0)
        {
            deiCount++;
        }
        else
        {
            deiCount = 0;
        }
        if (GeneralFunctions.instance.PlayerAugmentCount("Adaptive Strikes") > 0)
        {
            adaptiveStrikes = false;
        }
        else
        {
            adaptiveStrikes = false;
        }
    }
}
