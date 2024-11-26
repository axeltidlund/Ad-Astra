using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour
{
    protected WeaponData _weaponData;
    protected ProjectileData _projectileData;
    protected PlayerInventory _playerInventory;

    protected float _spawnTime => Time.fixedTime;
    protected Transform _origin;

    public Collider2D mainCollider;

    protected Rigidbody2D _rb;
    public float _timeLeft { get; protected set; }

    private int _ricochets = 0;
    private int _penetrations = 0;
    private float _lastHit = 0;

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
        _ricochets = _projectileData.ricochets;
        _penetrations = _projectileData.penetration;

        if (GeneralFunctions.instance.PlayerHasAugment("AP Ammo"))
        {
            _penetrations += 5;
        }

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
            this.gameObject.SetActive(false);
            Destroy(this.gameObject, 1f);
        }
        AI();

        LayerMask hitLayers = LayerMask.GetMask("Walls") | LayerMask.GetMask("Enemies");
        RaycastHit2D hit = Physics2D.CircleCast(this.transform.position, _projectileData.radius, -transform.right, _rb.velocity.magnitude * Time.fixedDeltaTime, hitLayers);

        if (!hit) return;
        if (hit.rigidbody.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            OnWall(hit);
        }
        else if (hit.rigidbody.gameObject.layer == LayerMask.NameToLayer("Enemies") && hit.collider.gameObject.tag == "Enemy")
        {
            ProcessHit(hit);
        }
    }

    private void ProcessHit(RaycastHit2D hit)
    {
        if (Time.fixedTime - _lastHit < _projectileData.maxAllowedHitFrequency) return;
        _lastHit = Time.fixedTime;

        // Damage
        Damageable damageable = hit.rigidbody.gameObject.GetComponent<Damageable>();
        if (damageable != null )
        {
            damageable.Damage(_weaponData.damage, _weaponData.reactiveType, gameObject.GetComponent<Rigidbody2D>().velocity.normalized * _weaponData.knockbackStrength, _weaponData.knockbackTime);
        }

        _penetrations -= 1;
        if (_penetrations <= 0)
        {
            OnHit();
            this.gameObject.SetActive(false);
            Destroy(this.gameObject, 1f);
        }
    }
    public virtual void AI() { }
    public virtual void OnHit() { }

    public virtual void OnWall(RaycastHit2D hitInfo) {
        if (_ricochets > 0 || GeneralFunctions.instance.PlayerHasAugment("Ricochet") ) {
            Vector2 newVelocity = Vector2.Reflect(_rb.velocity, hitInfo.normal);
            if (GeneralFunctions.instance.PlayerHasAugment("Rebound Flirt"))
            {
                newVelocity *= 1.2f;
            }

            _rb.velocity = newVelocity;

            if (GeneralFunctions.instance.PlayerHasAugment("Ricochet")) { return; }
            _ricochets -= 1;
        } else
        {
            OnHit();
            this.gameObject.SetActive(false);
            Destroy(this.gameObject, 1f);
        }
    }
}
