using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    public int _level = 1;
    public float _xp = 0f;

    public UnityEvent<int, int> levelChangedEvent;
    public UnityEvent<float, float> xpChangedEvent;

    public AudioClip xpGain;
    public AudioClip levelGain;

    private float RequiredXPForLevel(int level) {
        return 8 * Mathf.Log(level) + 10;
    }

    public void GiveXP(float amount) {
        xpChangedEvent.Invoke(Mathf.Min(1, (_xp + amount) / RequiredXPForLevel(_level)), Mathf.Min(1, _xp / RequiredXPForLevel(_level)));
        _xp += amount;
        GeneralFunctions.instance.PlaySound(xpGain, .8f, transform);
        if (_xp >= RequiredXPForLevel(_level)) {
            SetLevel(_level + 1);
            GeneralFunctions.instance.PlaySound(levelGain, 1f, transform);
        }
    }

    public void SetLevel(int level) {
        levelChangedEvent.Invoke(level, _level);
        _level = level;
        xpChangedEvent.Invoke(0f, Mathf.Min(1, _xp / RequiredXPForLevel(_level)));
        _xp = 0f;
    }
}
