using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    [SerializeField] bool lockMouse = true;
    public Vector2 MouseDelta;
    public Vector2 MoveComposite;

    private void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        MouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveComposite = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
    }
}
