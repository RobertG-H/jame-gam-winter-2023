using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCreeping : MonoBehaviour
{
    [SerializeField] float creepingSpeed = 2f;
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

        // Use the movement input to move the object
        transform.position += new Vector3(movementInput.x, 0, movementInput.y) * Time.deltaTime * creepingSpeed;
    }
}
