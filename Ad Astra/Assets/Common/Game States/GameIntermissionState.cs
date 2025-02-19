using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameIntermissionState : State
{
    public Animator uiAnimator;
    public Transform holder;
    public LootHandler lootHandler;
    public override void Enter()
    {
        isComplete = false;
        uiAnimator.SetBool("Selecting", true);

        foreach (Transform child in holder)
        {
            TextMeshProUGUI name = child.Find("Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI type = child.Find("Type").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI desc = child.Find("Description").GetComponent<TextMeshProUGUI>();
            UnityEngine.UI.Image icon = child.Find("Icon").GetComponent<UnityEngine.UI.Image>();

            Data dataHolder = lootHandler.Roll();
            if (dataHolder is WeaponData) {
                WeaponData weaponData = (WeaponData)dataHolder;

                name.text = weaponData.displayName;
                type.text = weaponData.rarity.ToString() + " Weapon";
                desc.text = weaponData.description;
            } else if (dataHolder is AugmentData) {
                AugmentData augmentData = (AugmentData)dataHolder;

                name.text = augmentData.displayName;
                type.text = augmentData.rarity.ToString() + " Augment";
                icon.sprite = augmentData.icon;
                desc.text = augmentData.description;
            }
        }
    }
    public override void Do()
    {
        
    }
    public override void Exit()
    {
        uiAnimator.SetBool("Selecting", false);
    }

    public void Click() {
        if (isComplete) return;
        isComplete = true;

        // Give 
    }
}
