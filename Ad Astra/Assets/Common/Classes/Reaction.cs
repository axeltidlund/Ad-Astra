using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction : MonoBehaviour
{
    public ReactionData reactionData;
    public virtual void Trigger(Transform position) { }
}
