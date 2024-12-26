using AYellowpaper.SerializedCollections;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralFunctions : MonoBehaviour
{
    public bool visualEffectsEnabled = true;
    public static GeneralFunctions instance;
    public GameObject player;

    public GameObject indicator;
    public GameObject textIndicator;

    public CinemachineVirtualCamera cam;
    private CinemachineBasicMultiChannelPerlin noise;

    public GameObject audioPrefab;

    [SerializedDictionary("Reactive Type", "Color")]
    public SerializedDictionary<Global.ReactiveType, Color> TypeColors;

    public float shakeDuration = 0f;
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
        }
        else
        {
            shakeDuration -= Time.deltaTime;
        }
    }
    public bool PlayerHasAugment(string augmentName)
    {
        bool hasAugment = false;

        if (player.GetComponent<PlayerInventory>().augmentations.Count == 0) return false;
        foreach (AugmentData augment in player.GetComponent<PlayerInventory>().augmentations)
        {
            if (augment.name.Equals(augmentName))
            {
                hasAugment = true;
            }
        }

        return hasAugment;
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
        go.transform.position = position + new Vector2(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f));

        DamageIndicator di = go.GetComponent<DamageIndicator>();
        di.Setup(text, duration);
    }
    public void ShakeCamera(float duration, float amplitude, float frequency)
    {
        if (instance.visualEffectsEnabled == false) { return; }
        shakeDuration = duration;
        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = frequency;
    }

    public void PlaySound(AudioClip clip, float volume, Transform _transform)
    {
        GameObject go = Instantiate(audioPrefab, _transform.position, Quaternion.identity);
        AudioSource source = go.GetComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume * .1f;
        source.loop = false;
        source.Play();
        Destroy(go, clip.length);
    }

    public float ApplyTransformativeReactionMultiplier(float damage, Global.AugmentReactionTarget reaction) {
        if (reaction == Global.AugmentReactionTarget.None) { return damage; }

        switch (reaction)
        {
            case Global.AugmentReactionTarget.Fusion:
                return damage * 2;
            default:
                return damage;
        }
    }
}
