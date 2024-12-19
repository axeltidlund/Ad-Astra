using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    private int _level = 0;
    private float _xp = 0f;

    public UnityEvent<int, int> levelChangedEvent;
    public UnityEvent<float, float> xpChangedEvent;

    private float RequiredXPForLevel(int level) {
        return 10 * Mathf.Log(level) + 10;
    }

    public void GiveXP(float amount) {
        xpChangedEvent.Invoke(_xp + amount, _xp);
        _xp += amount;
        if (_xp >= RequiredXPForLevel(_level)) {
            SetLevel(_level + 1);
        }
    }

    public void SetLevel(int level) {
        levelChangedEvent.Invoke(level, _level);
        _level = level;
    }
}
