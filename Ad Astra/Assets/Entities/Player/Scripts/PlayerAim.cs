using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    public Transform aimTransform;
    public Transform weaponHolder;

    public float recoilTime = 0;
    public float recoilStrength = 0;

    private float currentRecoil = 0;

    public Vector3 readAim { get; private set; } = Vector3.zero;

    Vector3 GetAim()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        Vector3 aim = new Vector3(0, 0, angle);
        if (Mathf.Sign(mousePosition.x - transform.position.x) == -1) 
        {
            weaponHolder.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            weaponHolder.localScale = Vector3.one;
        }

        return aim;
    }

    public void Recoil(float strength, float time)
    {
        float oldRecoil = recoilStrength * recoilTime;
        recoilTime = Mathf.Min(1, time);
        recoilStrength = Mathf.Min(359, strength);

        Vector3 aim = GetAim();
        readAim = aim;
        currentRecoil = (recoilStrength + oldRecoil) * weaponHolder.localScale.y;
        aimTransform.eulerAngles = aim + new Vector3(0, 0, currentRecoil);
    }

    void Update()
    {
        Vector3 aim = GetAim();
        readAim = aim;

        if (recoilTime <= 0)
        {
            recoilTime = 0;
            aimTransform.eulerAngles = aim;
        }
        else
        {
            aimTransform.eulerAngles = Vector3.Lerp(aim, aim + new Vector3(0, 0, currentRecoil), recoilTime);
            recoilTime -= Time.deltaTime;
        }
    }
}
