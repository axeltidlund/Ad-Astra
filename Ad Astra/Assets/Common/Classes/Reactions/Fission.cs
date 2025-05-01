using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fission : Reaction
{
    public ProjectileShooter shooter;
    public override void Trigger(Transform position)
    {
        reactionData.Sound(position);
        reactionData.Shake();
        for (int i = 0; i < 8; i++)
        {
            float angle = (360 / 8) * i;
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, angle));
            shooter.FireProjectile(reactionData, transform);
        }
    }
}
