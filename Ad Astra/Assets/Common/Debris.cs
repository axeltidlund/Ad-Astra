using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    public float time = 1f;

    private void Awake()
    {
        Destroy(gameObject, time);
    }
}
