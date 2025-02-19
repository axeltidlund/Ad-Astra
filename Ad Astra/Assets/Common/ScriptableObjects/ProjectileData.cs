using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjects/ProjectileData")]
public class ProjectileData : Data
{
    public GameObject projectilePrefab;

    public float travelSpeed;
    public float timeLeft;
    public float radius = 1;

    public int ricochets;
    public int penetration;

    public float maxAllowedHitFrequency = .1f;
}
