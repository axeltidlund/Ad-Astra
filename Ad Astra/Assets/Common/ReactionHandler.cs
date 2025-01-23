using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionHandler : MonoBehaviour
{
    [SerializedDictionary("Reaction Name", "Reaction")]
    public SerializedDictionary<string, Reaction> TypeColors;
}
