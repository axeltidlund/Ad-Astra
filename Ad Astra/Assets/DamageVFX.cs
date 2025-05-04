using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVFX : MonoBehaviour
{
    bool waiting;
    public GameLoop gameLoop;
    public AudioClip sound;

    public void PlaySound()
    {
        GeneralFunctions.instance.PlaySound(sound, 1f, transform);
    }
    public void Stop(float duration)
    {
        if (waiting)
            return;
        if (gameLoop.ended)
            return;
        Time.timeScale = 0.0f;
        StartCoroutine(Wait(duration));
    }

    IEnumerator Wait(float duration)
    {
        if (gameLoop.ended)
            yield return null;
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        if (gameLoop.ended)
            yield return null;
        Time.timeScale = 1.0f;
        waiting = false;
    }
}
