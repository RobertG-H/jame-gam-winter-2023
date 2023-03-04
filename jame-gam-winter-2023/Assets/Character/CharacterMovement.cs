using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    [SerializeField] float MovementSpeed;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 moveDirection)
    {
        rb.AddForce(moveDirection * MovementSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);
    }

}