using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    [SerializeField] VoidEventChannelSO gameStartEventChannel;
    [SerializeField] bool controlsEnabled = true;
    public Vector2 MouseDelta;
    public Vector2 MoveComposite;

    private void OnEnable ()
    {
        gameStartEventChannel.OnEvent += OnGameStart;
    }

    private void OnDisable ()
    {
        gameStartEventChannel.OnEvent -= OnGameStart;
    }

    void OnGameStart()
    {
        Cursor.lockState = CursorLockMode.Locked;
        controlsEnabled = true;
    }

    private void Start ()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!controlsEnabled)
            return;
        MouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!controlsEnabled)
            return;
        MoveComposite = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!controlsEnabled)
            return;
        if (!context.performed)
            return;
    }
}
