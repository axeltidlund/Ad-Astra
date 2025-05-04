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

        int max = 8;
        max = Mathf.FloorToInt(8 * Mathf.Pow(1.2f, GeneralFunctions.instance.PlayerAugmentCount("Chainbreaker")));

        for (int i = 0; i < max; i++)
        {
            float angle = (360 / max) * i;
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, angle));
            shooter.FireProjectile(reactionData, transform);
        }
    }
}
