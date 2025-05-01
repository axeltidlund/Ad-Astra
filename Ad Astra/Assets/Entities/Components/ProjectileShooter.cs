using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public List<ProjectileData> projectiles;
    public void FireProjectile(Data data, Transform origin, int i = 0)
    {
        GameObject projectileInstance = Instantiate(projectiles[i].projectilePrefab);
        projectileInstance.GetComponent<Projectile>().Spawn(data, projectiles[i], origin);
    }
}
