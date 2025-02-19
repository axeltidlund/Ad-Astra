using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceHandler : MonoBehaviour
{
    public Slider xpSlider;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI ammoText;
    public PlayerInventory playerInventory;
    public GameLoop gameLoop;
    public GameObject notification;

    private Weapon _lastWeaponHandler;

    public float xpGoal = 0f;

    public void LevelChanged(int newLevel, int oldLevel) {
        levelText.text = "Level: " + newLevel.ToString();
    }
    public void XPChanged(float newXp, float oldXp) {
        xpGoal = newXp; // lerp
    }
    public void WeaponChanged(Weapon weapon)
    {
        _lastWeaponHandler = weapon;
    }
    void Awake() {
        xpGoal = 0f;
        levelText.text = "Level: " + 1.ToString();
        playerInventory.switchWeapon += WeaponChanged;
    }
    void Update() {
        if (gameLoop.selectionsLeft > 0) {
            notification.SetActive(true);
        } else {
            notification.SetActive(false);
        }
        xpSlider.value = Mathf.Lerp(xpSlider.value, xpGoal, .25f * Time.deltaTime * 60);

        if (_lastWeaponHandler == null) { return; }
        if (_lastWeaponHandler is RangedWeapon)
        {
            RangedWeapon _rangedWeaponHandler = (RangedWeapon)_lastWeaponHandler;
            RangedWeaponData _rangedWeaponData = (RangedWeaponData)_rangedWeaponHandler.weaponData;

            ammoText.text = "ammo: " + _rangedWeaponHandler.ammo + " / " + _rangedWeaponData.magazineAmount;
        } else
        {
            ammoText.text = "";
        }
    }
}
