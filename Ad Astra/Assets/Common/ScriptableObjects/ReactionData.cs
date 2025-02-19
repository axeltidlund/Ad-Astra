using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ReactionData", menuName = "ScriptableObjects/ReactionData")]
public class ReactionData : Data
{
    public string displayName;
    [TextArea(5, 10)]
    public string description;

    public float damage;

    public Global.ReactiveType reactiveType;
    public GameObject prefab;

    public float shakeDuration;
    public float shakeSpeed;
    public float shakeAmp;

    public AudioClip sound;

    public void Shake()
    {
        GeneralFunctions.instance.ShakeCamera(shakeDuration, shakeAmp, shakeSpeed);
    }
    public void Sound(Transform origin)
    {
        if (!sound) return; 
        GeneralFunctions.instance.PlaySound(sound, 1f, origin);
    }
}
