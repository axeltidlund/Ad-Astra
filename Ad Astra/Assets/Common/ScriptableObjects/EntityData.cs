using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "ScriptableObjects/EntityData/Base")]
public class EntityData : Data
{
    public string displayName;

    public float maxHealth;

    public float speed;
    public float attack;
    public float globalRes = 1;
}
