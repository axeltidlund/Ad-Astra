using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatAugmentData : AugmentData
{
    public float amount;
    public bool isAdditive;

    public virtual float Apply(float stat)
    {
        return isAdditive ? stat += amount: stat *= amount;
    }
}
