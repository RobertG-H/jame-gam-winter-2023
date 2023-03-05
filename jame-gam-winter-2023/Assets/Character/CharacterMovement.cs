using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    [SerializeField] bool debug;
    [SerializeField] float MovementSpeed;
    [SerializeField] float WallCheckDist;
    [SerializeField] float GravityScale;
    [SerializeField] float StickRotationRate;
    [SerializeField] float LookRotationDampFactor;
    [SerializeField] float RbDrag;
    [SerializeField] float RbDragFlying;
    [SerializeField] Transform wallCheck;
    [SerializeField] Transform groundCheckFront;
        
    Rigidbody rb;
    RaycastHit hit;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(groundCheckFront.position, -transform.up, WallCheckDist);
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
        if(!IsGrounded())
            rb.drag = RbDragFlying;
        else
            rb.drag = RbDrag;
        Vector3 gravity = GetGravityVector() * GravityScale;
        rb.velocity += gravity * Time.fixedDeltaTime;
    }
    Vector3 GetGravityVector()
    {
        if(IsGrounded())
        {
            return -transform.up;
        }
        else {
            return Vector3.down;
        }
    }
    public void OrientWithGravity()
    {
        AlignWithSurface(-GetGravityVector());
    }

    public void TryWallClimb()
    {

        // Climb down wall if front doesn't hit, but back does
        if(!RaycastWithDebug(groundCheckFront.position, -transform.up, WallCheckDist))
        {
            if(RaycastWithDebug(groundCheckFront.position - transform.up, -transform.forward, out hit, WallCheckDist))
                AlignWithSurface(hit.normal);
        }
        
        if(RaycastWithDebug(wallCheck.position, transform.forward, out hit, WallCheckDist))
        {
            AlignWithSurface(hit.normal);
        }
        else if(RaycastWithDebug(groundCheckFront.position, -transform.up, out hit, WallCheckDist))
        {
            AlignWithSurface(hit.normal);
        }
    }
    void AlignWithSurface(Vector3 normal)
    {
        Vector3 alignTo = Vector3.Cross(transform.right, normal);
        Quaternion rotation = Quaternion.FromToRotation(transform.forward, alignTo);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation * transform.rotation, StickRotationRate * Time.fixedDeltaTime);
    }

    public bool RaycastWithDebug(Vector3 from, Vector3 dir, float dist)
    {
        bool result = Physics.Raycast(from, dir, dist);
        DrawDebugRays(result, from, dir, dist);
        return result;
    }
    public bool RaycastWithDebug(Vector3 from, Vector3 dir, out RaycastHit hit, float dist)
    {
        bool result = Physics.Raycast(from, dir, out hit, dist);
        DrawDebugRays(result, from, dir, dist);
        return result;
    }
    public void DrawDebugRays(bool didHit, Vector3 from, Vector3 dir, float dist)
    {
        if(debug)
        {
            if(didHit)
                Debug.DrawRay(from, dir * dist, Color.green);
            else
                Debug.DrawRay(from, dir * dist, Color.red);
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