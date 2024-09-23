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

        transform.SetPositionAndRotation(origin.position, origin.rotation);
    }
    public void Update()
    {
        _timeLeft -= Time.deltaTime;
        if (_timeLeft < 0)
        {
            Destroy(this.gameObject);
        }
        AI();
    }
    public virtual void AI() { }
}
