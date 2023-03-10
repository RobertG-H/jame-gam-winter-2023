using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    [SerializeField] VoidEventChannelSO gameStartEventChannel;
    [SerializeField] VoidEventChannelSO deathEventChannel;
    [SerializeField] VoidEventChannelSO interactEventChannel;
    [SerializeField] VoidEventChannelSO fireEventChannel;

    [SerializeField] Multiplizer multiplizer;

    bool controlsEnabled = false;
    public Vector2 MouseDelta;
    public Vector2 MoveComposite;

    private void OnEnable ()
    {
        gameStartEventChannel.OnEvent += OnGameStart;
        deathEventChannel.OnEvent += DisableControls;
    }

    private void OnDisable ()
    {
        gameStartEventChannel.OnEvent -= OnGameStart;
        deathEventChannel.OnEvent -= DisableControls;
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
        multiplizer.Fire ();
        Debug.Log ("FIRE");
        fireEventChannel.RaiseEvent ();

    }

    public void OnReset(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        GameManager.Instance.ResetGame ();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        interactEventChannel.RaiseEvent ();
    }

    public void DisableControls()
    {
        controlsEnabled = false;
    }
}
