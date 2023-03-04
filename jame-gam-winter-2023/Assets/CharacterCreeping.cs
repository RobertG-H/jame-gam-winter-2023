using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCreeping : MonoBehaviour
{
    [SerializeField] float creepingSpeed = 2f;
    [SerializeField] Camera cam;
    PlayerInput playerInput;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current movement input from the PlayerInput component
        Vector2 movementInput = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 movementDirection = Vector3.zero;
        Vector3 cameraDirection = new Vector3(cam.transform.forward.x,0,cam.transform.forward.z).normalized;
        Vector3 leftInput = Vector3.Cross(cameraDirection, Vector3.up);
        Vector3 rightInput = -leftInput;
        if (movementInput.x > 0)
            if (movementInput.y > 0)
                movementDirection = (rightInput + cameraDirection).normalized;
            else if (movementInput.y < 0)
                movementDirection = (rightInput - cameraDirection).normalized;
            else
                movementDirection = rightInput;
        else if (movementInput.x < 0)
            if (movementInput.y > 0)
                movementDirection = (leftInput + cameraDirection).normalized;
            else if (movementInput.y < 0)
                movementDirection = (leftInput - cameraDirection).normalized;
            else
                movementDirection = leftInput;
        else
            if (movementInput.y > 0)
                movementDirection = cameraDirection;
            else if (movementInput.y < 0)
                movementDirection = -cameraDirection;
            else
                movementDirection = Vector3.zero;

        // Use the movement input to move the object
        transform.position += (movementDirection) * Time.deltaTime * creepingSpeed;
    }
}
