using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float cameraSensitivity = 5f;
    PlayerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cameraMovementInput = playerInput.actions["Look"].ReadValue<Vector2>();

        cam.transform.RotateAround(transform.position,Vector3.up, -cameraMovementInput.x /cameraSensitivity);
    }
}
