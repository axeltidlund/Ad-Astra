using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour
{
    protected WeaponData _weaponData;
    protected ProjectileData _projectileData;

    protected float _spawnTime => Time.fixedTime;
    protected Transform _origin;

    public Collider2D mainCollider;

    protected Rigidbody2D _rb;
    public float _timeLeft { get; protected set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public virtual void Spawn(WeaponData weaponData, ProjectileData projectileData, Transform origin)
    {
        _weaponData = weaponData;
        _origin = origin;
        _projectileData = projectileData;
        _timeLeft = _projectileData.timeLeft;

        float spread = 0;
        if (weaponData is RangedWeaponData)
        {
            RangedWeaponData rangedData = weaponData as RangedWeaponData;
            spread = rangedData.spread;
        }
        transform.SetPositionAndRotation(origin.position, origin.rotation * Quaternion.Euler(0, 0, Random.Range(-spread, spread)));
    }
    public void Update()
    {
        _timeLeft -= Time.deltaTime;
        if (_timeLeft < 0)
        {
            OnKill();
            this.gameObject.SetActive(false);
            Destroy(this.gameObject, 1f);
        }
        AI();

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, _rb.velocity, _rb.velocity.magnitude * Time.fixedDeltaTime, LayerMask.GetMask("Walls"));
        if (hit) {
            OnWall(hit);
        }
    }
    public virtual void AI() { }
    public virtual void OnKill() { }

    public virtual void OnWall(RaycastHit2D hitInfo) { }
}
