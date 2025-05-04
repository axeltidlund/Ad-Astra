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
    public PlayerInventory inventory;

    public DataHolder button1;
    public DataHolder button2;
    public DataHolder button3;

    Data TryRoll(List<Data> alreadyPicked, int attempts) {
        if (attempts <= 0) return new Data();
        Data dataHolder = lootHandler.Roll();
        if (alreadyPicked.Contains(dataHolder)) return TryRoll(alreadyPicked, attempts - 1);
        if (dataHolder is AugmentData) {
            if (!(dataHolder as AugmentData).stackable && GeneralFunctions.instance.PlayerAugmentCount(dataHolder.name) > 0) return TryRoll(alreadyPicked, attempts - 1);
        }
        return dataHolder;
    }
    public override void Enter()
    {
        isComplete = false;
        uiAnimator.SetBool("Selecting", true);
        List<Data> alreadyPicked = new List<Data>();

        foreach (Transform child in holder)
        {
            child.gameObject.SetActive(true);
            TextMeshProUGUI name = child.Find("Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI type = child.Find("Type").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI desc = child.Find("Description").GetComponent<TextMeshProUGUI>();
            UnityEngine.UI.Image icon = child.Find("Icon").GetComponent<UnityEngine.UI.Image>();

            Data dataHolder = TryRoll(alreadyPicked, 10);
            alreadyPicked.Add(dataHolder);
            if (dataHolder is WeaponData) {
                WeaponData weaponData = (WeaponData)dataHolder;

                name.text = weaponData.displayName;
                icon.sprite = weaponData.gunPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
                type.text = weaponData.rarity.ToString() + " Weapon";
                type.color = GeneralFunctions.instance.RarityColors[weaponData.rarity];
                desc.text = weaponData.description;
            } else if (dataHolder is AugmentData) {
                AugmentData augmentData = (AugmentData)dataHolder;

                name.text = augmentData.displayName;
                type.text = augmentData.rarity.ToString() + " Augment";
                type.color = GeneralFunctions.instance.RarityColors[augmentData.rarity];
                icon.sprite = augmentData.icon;
                desc.text = augmentData.description;
            } else {
                child.gameObject.SetActive(false);
            }
            if (dataHolder)
            {
                child.GetComponent<DataHolder>().data = dataHolder;
            }
        }
    }
    public override void Do()
    {

    }
    public override void Exit()
    {
        uiAnimator.SetBool("Selecting", false);

        foreach (Transform child in holder)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void Click1() {
        if (isComplete) return;
        isComplete = true;

        // Give
        if (!button1.data) return;
        Give(button1.data);
    }
    public void Click2()
    {
        if (isComplete) return;
        isComplete = true;

        // Give
        if (!button2.data) return;
        Give(button2.data);
    }
    public void Click3()
    {
        if (isComplete) return;
        isComplete = true;

        // Give
        if (!button3.data) return;
        Give(button3.data);
    }
    public void Give(Data data)
    {
        inventory.Give(data);
    }
}
