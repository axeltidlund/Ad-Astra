using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    TMP_Text text;
    float duration = 2;
    bool setup = false;

    Vector2 originalPosition;
    void Awake() {
        text = GetComponent<TMP_Text>();
        text.alpha = 1.0f;
    }
    public void Setup(string number, float _duration)
    {
        originalPosition = transform.position;
        text.SetText(number);
        duration = _duration;
        setup = true;
    }
    void Update() {
        if (!setup) { return; }
        text.alpha -= Time.deltaTime / duration;
        if (text.alpha <= 0)
        {
            Destroy(gameObject);
            return;
        }
    }
}
