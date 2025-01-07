using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    public Transform aimTransform;
    public Transform weaponHolder;

    public float recoilTime = 0;
    public float recoilStrength = 0;

    public void UpdateAim(Vector2 targetPosition) {
        Vector3 aimDirection = (targetPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        Vector3 aim = new Vector3(0, 0, angle);
        if (Mathf.Sign(targetPosition.x - transform.position.x) == -1) 
        {
            weaponHolder.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            weaponHolder.localScale = Vector3.one;
        }
        aimTransform.eulerAngles = aim;
    }
}
