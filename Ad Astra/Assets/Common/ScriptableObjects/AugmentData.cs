using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomAugmentData", menuName = "ScriptableObjects/Augments/CustomAugmentData")]
public class AugmentData : ScriptableObject
{
    public Texture2D icon;
    public string displayName;
    [TextArea(5, 10)]
    public string description;

    public Global.Rarities rarity;
}
