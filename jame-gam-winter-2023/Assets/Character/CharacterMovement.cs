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
    RaycastHit hit;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public bool IsGrounded()
    {
        return true;
    }

    public void Move(float moveMagnitude)
    {
        Vector3 dir = transform.forward * moveMagnitude;   
        Debug.DrawRay(transform.position, dir * 5f, Color.green, 1f);
        rb.AddForce(dir * MovementSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);
    }

    public void ApplyGravity()
    {
        Vector3 gravity = -transform.up * gravityScale;
        rb.velocity += gravity * Time.fixedDeltaTime;
    }

    public void TryWallClimb()
    {

        // Climb down wall if front doesn't hit, but back does
        if(!RaycastWithDebug(groundCheckFront.position, -transform.up, wallCheckDist) && 
            RaycastWithDebug(groundCheckBack.position, -transform.up, wallCheckDist))
        {
            transform.Rotate(Vector3.right * rotationRate, Space.Self);
        }
        
        if(Physics.Raycast(wallCheck.position, transform.forward, out hit, wallCheckDist))
        {
            AlignWithSurface(hit.normal);
        }
        else if(Physics.Raycast(groundCheckFront.position, -transform.up, out hit, wallCheckDist))
        {
            AlignWithSurface(hit.normal);
        }

    }
    void AlignWithSurface(Vector3 normal)
    {
        Vector3 alignTo = Vector3.Cross(transform.right, normal);
        Quaternion rotation = Quaternion.FromToRotation(transform.forward, alignTo);
        transform.rotation = rotation * transform.rotation;
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