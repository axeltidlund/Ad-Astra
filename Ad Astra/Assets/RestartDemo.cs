using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartDemo : MonoBehaviour
{
    bool debounce = false;
    void Update()
    {
        if (debounce) return;
        if (Input.GetKeyDown(KeyCode.Keypad0)) {
            SceneManager.LoadScene(0);
            debounce = true;
        }
    }
}
