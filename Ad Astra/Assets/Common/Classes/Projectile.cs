using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour
{
    protected Data _weaponData;
    protected ProjectileData _projectileData;
    protected PlayerInventory _playerInventory;
    public float _travelSpeed;

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
    public virtual void Spawn(Data weaponData, ProjectileData projectileData, Transform origin)
    {
        _weaponData = weaponData;
        _origin = origin;
        _projectileData = projectileData;
        _travelSpeed = _projectileData.travelSpeed;
        _timeLeft = _projectileData.timeLeft;
        _ricochets = _projectileData.ricochets;
        _penetrations = _projectileData.penetration;

        if (GeneralFunctions.instance.PlayerAugmentCount("Tricks of the Troublemaker") > 0)
        {
            _travelSpeed *= Random.value * 2f;
        }

        int augmentCount = GeneralFunctions.instance.PlayerAugmentCount("AP Ammo");
        if (augmentCount > 0)
        {
            _penetrations += 5 * augmentCount;
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
            if (_weaponData is WeaponData)
            {
                WeaponData weaponData = (WeaponData)_weaponData;
                damageable.Damage(weaponData.damage, weaponData.reactiveType, gameObject.GetComponent<Rigidbody2D>().velocity.normalized * weaponData.knockbackStrength, weaponData.knockbackTime);
            } else if (_weaponData is ReactionData)
            {
                ReactionData weaponData = (ReactionData)_weaponData;
                damageable.Damage(weaponData.damage, weaponData.reactiveType, gameObject.GetComponent<Rigidbody2D>().velocity.normalized * 2f, 1f);
            }
        }

        _penetrations -= 1;
        if (_penetrations <= 0)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject, 1f);
        }

        OnHit();
    }
    public virtual void AI() { }
    public virtual void OnHit() { }

    public virtual void OnWall(RaycastHit2D hitInfo) {
        if (Time.fixedTime - _lastHit < _projectileData.maxAllowedHitFrequency) return;
        _lastHit = Time.fixedTime;
        
        if (_ricochets > 0 || GeneralFunctions.instance.PlayerAugmentCount("Ricochet") > 0 ) {
            Vector2 newVelocity = Vector2.Reflect(_rb.velocity, hitInfo.normal);

            int augmentCount = GeneralFunctions.instance.PlayerAugmentCount("Rebound Flirt");
            if (augmentCount > 0)
            {
                newVelocity *= 1 + (.2f * ((1 - Mathf.Pow(Global.diminishRate, augmentCount))/(1 - Global.diminishRate)));
            }

            _rb.velocity = newVelocity;

            if (GeneralFunctions.instance.PlayerAugmentCount("Ricochet") > 0 ) { return; }
            _ricochets -= 1;
        } else
        {
            OnHit();
            this.gameObject.SetActive(false);
            Destroy(this.gameObject, 1f);
        }
    }
}
