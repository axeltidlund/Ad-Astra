using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    public float outlineWidth = 0f;
    public Color outlineColor;

    private Material mat;
    private void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        mat = spriteRenderer.material;
    }
    public void UpdateOutline(float width, Color color)
    {
        if (!GeneralFunctions.instance.visualEffectsEnabled) { return; }
        outlineWidth = width;
        outlineColor = color;

        mat.SetFloat("_Offset", outlineWidth);
        mat.SetColor("_ElementColor", outlineColor);
    }
}
