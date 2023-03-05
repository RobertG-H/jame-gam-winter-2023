using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailSegment : MonoBehaviour
{
    int layerMask;
    void Awake()
    {
        layerMask = ~LayerMask.GetMask("Player");
    }
    public bool RaycastSegment(out RaycastHit hit, float stickDistance)
    {
        Debug.DrawRay(transform.position, -transform.forward * stickDistance, Color.red);
        return Physics.Raycast(transform.position, -transform.forward, out hit, stickDistance, layerMask);
    }
    public void StickToPosition(RaycastHit hit)
    {
        transform.position = Vector3.Lerp(transform.position, hit.point, 0.5f);

        Vector3 lookAt = Vector3.Cross(hit.normal, transform.right);
        Debug.DrawRay(transform.position, lookAt * 5f, Color.green, 5f);
        // reverse it if it is down.
        // lookAt = lookAt.y < 0 ? -lookAt : lookAt;
        // look at the hit's relative up, using the normal as the up vector
        transform.rotation = Quaternion.LookRotation(lookAt, hit.normal);
    }
}
