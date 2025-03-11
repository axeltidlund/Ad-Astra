using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class HitboxHandler : MonoBehaviour
{
    public int step = 2;
    public List<RaycastHit2D> Angular(int angle, Transform origin, float radius, bool isEnemy = false)
    {
        LayerMask hitLayers = LayerMask.GetMask("Walls") | LayerMask.GetMask("Enemies") | LayerMask.GetMask("Player");

        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        HashSet<GameObject> uniqueObjects = new HashSet<GameObject>();

        int extreme = (int)(angle / 2);

        for (int i = -extreme; i < extreme; i += step)
        {
            Vector2 dir = (Vector2)(origin.rotation * Quaternion.Euler(0, 0, i) * Vector3.right);
            RaycastHit2D[] angleHits = Physics2D.CircleCastAll(transform.position, 0.1f, dir.normalized, radius, hitLayers);
            Debug.DrawRay(transform.position, dir * radius, Color.green, 1f);

            foreach (RaycastHit2D hit in angleHits)
            {
                if ((!isEnemy && (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemies") &&
                    hit.collider.CompareTag("Enemy") && hit.rigidbody != null)) || (isEnemy && (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player") &&
                    hit.collider.CompareTag("Player") && hit.rigidbody != null)))
                {
                    if (uniqueObjects.Add(hit.rigidbody.gameObject))
                    {
                        hits.Add(hit);
                    }
                }
            }
        }

        return hits;
    }

    public List<RaycastHit2D> Rect(int angle, Transform origin, float radius, float length, bool isEnemy = false)
    {
        LayerMask hitLayers = LayerMask.GetMask("Walls") | LayerMask.GetMask("Enemies") | LayerMask.GetMask("Player");
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        HashSet<GameObject> uniqueObjects = new HashSet<GameObject>();

        Vector2 dir = (Vector2)(origin.rotation * Vector3.right);
        RaycastHit2D[] angleHits = Physics2D.CircleCastAll(transform.position, radius, dir.normalized, radius, hitLayers);
        Debug.DrawRay(transform.position, dir * length, Color.green, 1f);

        foreach (RaycastHit2D hit in angleHits)
        {
            if ((!isEnemy && (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemies") &&
                   hit.collider.CompareTag("Enemy") && hit.rigidbody != null)) || (isEnemy && (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player") &&
                   hit.collider.CompareTag("Player") && hit.rigidbody != null)))
            {
                if (uniqueObjects.Add(hit.rigidbody.gameObject))
                {
                    hits.Add(hit);
                }
            }
        }

        return hits;
    }
}
