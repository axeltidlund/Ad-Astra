using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionHandler : MonoBehaviour
{
    [SerializedDictionary("Reaction", "Object")]
    public SerializedDictionary<Global.AugmentReactionTarget, ReactionData> Reactions;

    public void Trigger(Global.AugmentReactionTarget reaction, Transform _transform)
    {
        if(Reactions.TryGetValue(reaction, out ReactionData obj))
        {
            GameObject go = Instantiate(obj.prefab);
            go.transform.position = _transform.position;
            go.GetComponent<Reaction>().Trigger(_transform);
        }
    }
}
