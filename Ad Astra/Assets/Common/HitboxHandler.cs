using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxHandler : MonoBehaviour
{
    public int step = 2;
    public List<RaycastHit2D> Angular(int angle, Transform origin, float radius)
    {
        LayerMask hitLayers = LayerMask.GetMask("Walls") | LayerMask.GetMask("Enemies");

        List<RaycastHit2D> hits = new List<RaycastHit2D>();

        int extreme = (int)(angle / 2);

        for (int i = -extreme; i < extreme; i += step)
        {
            Vector3 dir = Quaternion.AngleAxis(i, origin.up) * origin.forward;
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, step, dir, radius, hitLayers);
            Debug.DrawRay(transform.position, dir, Color.green, 1f, true);
            Debug.Log(dir);

            if (!hit) continue;
            if (hit.rigidbody.gameObject.layer == LayerMask.NameToLayer("Enemies") && hit.collider.gameObject.tag == "Enemy" && hits.FindIndex(go => go.collider == hit.collider) == -1)
            {
                hits.Add(hit);
                Debug.Log(hit.collider.gameObject);
            }
        }

        return hits;
    }
}
