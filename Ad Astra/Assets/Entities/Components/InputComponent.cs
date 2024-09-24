using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputComponent : MonoBehaviour
{
    public Vector2 movementInput { get; protected set; }

    public UnityEvent mouseDown;
    public UnityEvent mouseUp;
    public void UpdateMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void MouseClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            mouseDown.Invoke();
        } else if (context.canceled)
        {
            mouseUp.Invoke();
        }
    }
}
