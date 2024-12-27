using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EntityData/Enemy")]
public class EnemyData : EntityData
{
    public float attackSpeed;
    public float detectionRange;
    public float xp;
}
