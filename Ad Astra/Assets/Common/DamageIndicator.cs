using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    TextMeshPro text;
    public float duration = 2;
    void Start() {
        text = GetComponent<TextMeshPro>();
    }
    void Update() {
        text.alpha += Time.deltaTime / duration;
    }
}
