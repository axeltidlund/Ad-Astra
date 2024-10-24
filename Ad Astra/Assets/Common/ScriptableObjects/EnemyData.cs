using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string displayName;

    public float speed;
    public float attackSpeed;

    public float attack;
    public float globalRes = 1;
}
