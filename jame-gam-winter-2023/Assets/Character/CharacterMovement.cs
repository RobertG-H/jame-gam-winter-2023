using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    [SerializeField] bool debug;
    [SerializeField] float MovementSpeed;
    [SerializeField] float wallCheckDist;
    [SerializeField] float gravityScale;
    [SerializeField] float rotationRate;
    [SerializeField] Transform wallCheck;
    [SerializeField] Transform groundCheckFront;
    [SerializeField] Transform groundCheckBack;
    
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public bool IsGrounded()
    {
        return true;
    }

    public void Move(Vector3 moveDirection)
    {
        //TODO: Add move direction
        rb.AddForce(moveDirection * MovementSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);
    }

    public void ApplyGravity()
    {
        Vector3 gravity = -transform.up * gravityScale;
        rb.velocity += gravity * Time.fixedDeltaTime;
    }

    public void TryWallClimb()
    {
        // Climb up wall
        if(RaycastWithDebug(wallCheck.position, transform.forward, wallCheckDist))
        {
            transform.Rotate(-Vector3.right * rotationRate, Space.Self);
        }
        // Climb down wall if front doesn't hit, but back does
        if(!RaycastWithDebug(groundCheckFront.position, -transform.up, wallCheckDist) && 
            RaycastWithDebug(groundCheckBack.position, -transform.up, wallCheckDist))
        {
            transform.Rotate(Vector3.right * rotationRate, Space.Self);
        }
    }


    public bool RaycastWithDebug(Vector3 from, Vector3 dir, float dist)
    {
        
        if(Physics.Raycast(from, dir, dist))
        {
            if(debug)
                Debug.DrawRay(from, dir * dist, Color.red);
            return true;
        }
        else {
            if(debug)
                Debug.DrawRay(from, dir * dist, Color.green);
            return false;
        }
    }
}