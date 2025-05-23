using AYellowpaper.SerializedCollections;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeneralFunctions : MonoBehaviour
{
    public bool visualEffectsEnabled = true;
    public static GeneralFunctions instance;
    public GameObject player;

    public GameObject indicator;
    public GameObject textIndicator;
    public ReactionHandler reactionHandler;
    public AugmentManager augmentManager;

    public CinemachineVirtualCamera cam;
    private CinemachineBasicMultiChannelPerlin noise;

    public GameObject audioPrefab;

    [SerializedDictionary("Reactive Type", "Color")]
    public SerializedDictionary<Global.ReactiveType, Color> TypeColors;

    [SerializedDictionary("Rarity", "Color")]
    public SerializedDictionary<Global.Rarities, Color> RarityColors;

    public float shakeDuration = 0f;
    private float maxShakeDuration = 1f;
    private float maxShakeAmp = 1f;

    public float globalDifficulty = 1f;
    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        if (cam != null)
        {
            noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }
    private void Update()
    {
        if (shakeDuration < 0f )
        {
            shakeDuration = 0f;
            noise.m_AmplitudeGain = 0f;
            maxShakeAmp = 1f;
            maxShakeDuration = 1f;
        }
        else
        {
            shakeDuration -= Time.deltaTime;
            noise.m_AmplitudeGain = maxShakeAmp * (Mathf.Pow((shakeDuration / maxShakeDuration), 2));
        }
    }
    public int PlayerAugmentCount(string augmentName)
    {
        int augmentCount = 0;

        if (player.GetComponent<PlayerInventory>().augmentations.Count == 0) return 0;
        foreach (AugmentData augment in player.GetComponent<PlayerInventory>().augmentations)
        {
            if (augment.name.Equals(augmentName))
            {
                augmentCount++;
            }
        }

        return augmentCount;
    }

    public List<AugmentData> GetAugmentDatas()
    {
        return player.GetComponent<PlayerInventory>().augmentations;
    }
    public void SpawnDamageIndicator(Vector2 position, float damage, float duration)
    {
        if (instance.visualEffectsEnabled == false) { return; }
        GameObject go = Instantiate(indicator);
        go.transform.position = position + new Vector2(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f));
        
        DamageIndicator di = go.GetComponent<DamageIndicator>();
        di.Setup(damage.ToString(), duration);
    }
    public void SpawnTextIndicator(Vector2 position, string text, float duration)
    {
        if (instance.visualEffectsEnabled == false) { return; }
        GameObject go = Instantiate(textIndicator);
        go.transform.position = position + new Vector2(Random.Range(-.5f, .5f) * -1, Random.Range(-.5f, .5f) * -1);

        DamageIndicator di = go.GetComponent<DamageIndicator>();
        di.Setup(text, duration);
    }
    public void ShakeCamera(float duration, float amplitude, float frequency)
    {
        if (instance.visualEffectsEnabled == false) return;
        if (duration <= shakeDuration) return;
        if (amplitude <= noise.m_AmplitudeGain) return;

        maxShakeDuration = duration;
        shakeDuration = duration;
        maxShakeAmp = amplitude;
        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = frequency;
    }

    public void PlaySound(AudioClip clip, float volume, Transform _transform)
    {
        GameObject go = Instantiate(audioPrefab, _transform.position, Quaternion.identity);
        AudioSource source = go.GetComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume * .1f;
        source.pitch = .8f + (Random.value * .4f);
        source.loop = false;
        source.Play();
        Destroy(go, clip.length);
    }

    public int enemyCount = 0;

    public float TriggerReaction(float damage, Global.AugmentReactionTarget reaction, Transform transform) {
        if (reaction == Global.AugmentReactionTarget.None) { return damage; }

        switch (reaction)
        {
            case Global.AugmentReactionTarget.Fusion:
                return damage * (2 * Mathf.Pow(1.3f, GeneralFunctions.instance.PlayerAugmentCount("Hearts Entwined")));
            default:
                reactionHandler.Trigger(reaction, transform);
                return damage;
        }
    }

    public void RewardXP(float amount) {
        Level level = player.GetComponent<Level>();
        level.GiveXP(amount);
    }

    public List<Rigidbody2D> tetherstormBullets = new List<Rigidbody2D>();
    public float forceMultiplier = 15f;
    public void AttractTetherbullet(Rigidbody2D bullet) {
        Vector2 totalForce = Vector2.zero;

        foreach (Rigidbody2D otherBullet in tetherstormBullets)
        {
            if (otherBullet == null) { continue; }
            if (otherBullet == bullet) { continue; }
            Vector2 force = (otherBullet.transform.position - bullet.transform.position).normalized;
            totalForce += force;
        }

        totalForce = totalForce.normalized * forceMultiplier;
        bullet.velocity += totalForce * Time.deltaTime;
    }
    public float StackValue(int amount)
    {
        return Mathf.Sqrt(amount);
    }
    public float ApplyAugmentDamageModifications(float damage)
    {
        return augmentManager.ApplyAugments(damage);
    }
    public float GetUseSpeed()
    {
        return augmentManager.useSpeed;
    }

    public UnityEvent onEnemyHit;
    public void EnemyHit()
    {
        onEnemyHit.Invoke();
    }

    float bonusAtZero = 2f;
    float exponent = 3f;
    public float PlayerHealthBuff()
    {
        Stats stats = player.GetComponent<Stats>();
        Damageable damageable = player.GetComponent<Damageable>();
        if (stats == null || damageable == null) { return 1f; }

        float maxHealth = stats.entityData.maxHealth;
        float health = damageable.health;
        float ratio = health / maxHealth;

        return 1f + Mathf.Pow(1f - ratio, exponent) * bonusAtZero;
    }

    public Global.ReactiveType GetWeaponReactiveType()
    {
        PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
        WeaponData data = playerInventory.weapons[playerInventory._currentWeaponIndex];
        if (data == null)
        {
            return Global.ReactiveType.None;
        }

        return data.reactiveType;
    }
}
