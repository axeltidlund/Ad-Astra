using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public EntityData entityData;

    Damageable damageable;

    private float globalRes;
    private Dictionary<string, float> resistances = new Dictionary<string, float>();

    [SerializeField]
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

    public Global.AugmentReactionTarget ApplyElement(Global.ReactiveType element)
    {
        if (element == Global.ReactiveType.None) { return Global.AugmentReactionTarget.None; }
        if (currentElement == element) { return Global.AugmentReactionTarget.None; }

        Global.AugmentReactionTarget reaction = Global.GetReaction(currentElement, element);

        OutlineController outlineController = GetComponentInChildren<OutlineController>();
        if (reaction == Global.AugmentReactionTarget.None)
        {
            outlineController?.UpdateOutline(.05f, GeneralFunctions.instance.TypeColors[element]);
            currentElement = element; 
            return Global.AugmentReactionTarget.None;
        }
        else
        {
            outlineController?.UpdateOutline(0f, GeneralFunctions.instance.TypeColors[Global.ReactiveType.None]);
            currentElement = Global.ReactiveType.None;
            Debug.Log(reaction);

            GeneralFunctions.instance.SpawnTextIndicator(transform.position, reaction.ToString(), 2f);
            return reaction;
        }
    }
}
