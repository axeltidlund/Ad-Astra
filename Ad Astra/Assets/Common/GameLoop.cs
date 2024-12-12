using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameLoop : MonoBehaviour
{
    public UnityEvent<string, string> stateChanged;

    private string _currentState = "INTERMISSION";
    private float _currentStateTimer = 0f;

    public void SetState(string newState) {
        stateChanged.Invoke(newState, _currentState);

        _currentState = newState;
        _currentStateTimer = 0f;
    }

    void Update() {
        _currentStateTimer += Time.deltaTime;
    }
}
