using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    TMP_Text text;
    float duration = 2;
    bool setup = false;

    float passed = 0;
    float dir1 = 0;
    float dir2 = 0;

    Vector2 originalPosition;
    void Awake() {
        text = GetComponent<TMP_Text>();
        text.alpha = 1.0f;
    }
    public void Setup(string number, float _duration)
    {
        originalPosition = transform.position;
        text.SetText(number);
        duration = _duration * Random.Range(.5f, 1.5f);
        setup = true;
        dir1 = Random.Range(-1.5f, 1.5f);
        dir2 = Random.Range(-1.5f, 1.5f);
    }
    void Update() {
        if (!setup) { return; }
        passed += Time.deltaTime / duration;
        text.alpha -= Time.deltaTime / duration;

        float x = dir1 * (1 - Mathf.Pow(1 - passed, (int)Random.Range(3, 8)));
        float y = dir2 * Mathf.Sin(passed * Mathf.PI * .5f);

        transform.position = originalPosition + new Vector2(x, y);

        if (text.alpha <= 0)
        {
            Destroy(gameObject);
            return;
        }
    }
}
