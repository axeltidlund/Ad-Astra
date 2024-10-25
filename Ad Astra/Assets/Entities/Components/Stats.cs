using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public EntityData entityData;

    private float globalRes;
    private Dictionary<string, float> resistances = new Dictionary<string, float>();
    private Global.ReactiveType currentElement = Global.ReactiveType.None;

    private void Awake()
    {
        globalRes = entityData.globalRes;
        string[] elements = Enum.GetNames(typeof(Global.ReactiveType));
        foreach (string element in elements)
        {
            resistances[element] = 1.0f;
        }
    }

    public float GetGlobalResistance() { return globalRes; }
    public float GetResistance(string element) { return resistances[element]; }

    public void ApplyElement(string element)
    {
        Global.ReactiveType parsedElement = (Global.ReactiveType)Enum.Parse(typeof(Global.ReactiveType), element);
        if (parsedElement != Global.ReactiveType.None) { return; }
        if (currentElement == parsedElement) { return; }

        Global.AugmentReactionTarget reaction = Global.GetReaction(currentElement, parsedElement);
        if (reaction == Global.AugmentReactionTarget.None) { return; }
    }
}
