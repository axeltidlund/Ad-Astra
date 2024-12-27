using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceHandler : MonoBehaviour
{
    public Slider xpSlider;
    public TextMeshProUGUI levelText;

    public float xpGoal = 0f;

    public void LevelChanged(int newLevel, int oldLevel) {
        levelText.text = "Level: " + newLevel.ToString();
    }
    public void XPChanged(float newXp, float oldXp) {
        xpGoal = newXp; // lerp
    }
    void Awake() {
        xpGoal = 0f;
        levelText.text = "Level: " + 1.ToString();
    }
    void Update() {
        xpSlider.value = Mathf.Lerp(xpSlider.value, xpGoal, .25f * Time.deltaTime * 60);
    }
}
