using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceHandler : MonoBehaviour
{
    public Slider xpSlider;
    public TextMeshProUGUI levelText;

    public void LevelChanged(int newLevel, int oldLevel) {
        levelText.text = "Level: " + newLevel.ToString();
    }
    public void XPChanged(float newXp, float oldXp) {
        xpSlider.value = newXp; // lerp
    }
    void Update() {

    }
}
