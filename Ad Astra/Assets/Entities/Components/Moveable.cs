using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Moveable : MonoBehaviour
{
    Rigidbody2D body;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 movement, float speed)
    {
        body.MovePosition(body.position + movement.normalized * speed * Time.fixedDeltaTime);
        body.velocity = Vector2.zero;
    }
}
