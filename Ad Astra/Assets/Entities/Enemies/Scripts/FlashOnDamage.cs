using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOnDamage : MonoBehaviour
{
    private Material mat;
    public float flashTime;
    private void Awake()
    {
        mat.SetInt("_Hit", 0);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        mat = spriteRenderer.material;
    }
    public void Do()
    {
        if (GeneralFunctions.instance.visualEffectsEnabled == false) { return; }
        StartCoroutine(Flash());
    }
    IEnumerator Flash()
    {
        mat.SetInt("_Hit", 1);
        yield return new WaitForSeconds(flashTime);
        mat.SetInt("_Hit", 0);
    }
}
