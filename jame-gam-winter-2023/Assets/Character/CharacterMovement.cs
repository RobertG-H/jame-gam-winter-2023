using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    [SerializeField] bool debug;
    [SerializeField] float MovementSpeed;
    [SerializeField] float WallCheckDist;
    [SerializeField] float GravityScale;
    [SerializeField] float RotationRate;
    [SerializeField] float LookRotationDampFactor;
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

    public void Move(Vector3 moveDirection, Vector3 input)
    {
        Vector3 inputInfluence = transform.forward * input.y + transform.right * input.x;
        Vector3 dir = Vector3.Lerp(moveDirection, inputInfluence, Mathf.Abs(Vector3.Dot(moveDirection.normalized, transform.up)));

        rb.AddForce(dir * MovementSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);
        FaceMoveDirection(dir);
    }

    public void ApplyGravity()
    {
        Vector3 gravity = -transform.up * GravityScale;
        rb.velocity += gravity * Time.fixedDeltaTime;
    }

    public void TryWallClimb()
    {

        // Climb down wall if front doesn't hit, but back does
        if(!RaycastWithDebug(groundCheckFront.position, -transform.up, WallCheckDist) && 
            RaycastWithDebug(groundCheckBack.position, -transform.up, WallCheckDist))
        {
            transform.Rotate(Vector3.right * RotationRate, Space.Self);
        }
        
        if(Physics.Raycast(wallCheck.position, transform.forward, out hit, WallCheckDist))
        {
            AlignWithSurface(hit.normal);
        }
        else if(Physics.Raycast(groundCheckFront.position, -transform.up, out hit, WallCheckDist))
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

    void FaceMoveDirection(Vector3 moveDirection)
    {

        Vector3 faceDirection = moveDirection.normalized;

        if (faceDirection == Vector3.zero)
            return;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(faceDirection, transform.up), LookRotationDampFactor * Time.deltaTime);

    }
}