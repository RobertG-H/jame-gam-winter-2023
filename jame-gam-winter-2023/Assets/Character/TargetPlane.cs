using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlane : MonoBehaviour
{
    Vector3 cachedHitPoint = Vector3.zero;
    int layerMask;
    void Awake()
    {
        layerMask = LayerMask.GetMask("Targetable");
    }
    public Vector3 RaycastToEdge(Vector3 cameraPosition, Vector3 cameraDirection)
    {
        RaycastHit hit;
        Debug.DrawRay (cameraPosition, cameraDirection * 100f, Color.black, 5);
        if (Physics.Raycast(cameraPosition, cameraDirection, out hit, Mathf.Infinity, layerMask)){
            //print("Found an object - distance: " + hit.distance);
            //print("Found an object: " + hit.collider.gameObject);
            GameObject targetObj = hit.collider.gameObject;

            Vector3 vecHit2Obj = transform.position - hit.point;
            cachedHitPoint = hit.point;
            return hit.point;
            
            // if (targetObj.tag == "TargetPlaneTag")
            // {
                //Debug.DrawRay (hit.point, vecHit2Obj * 100f, Color.cyan, 5f);
                //Debug.Log ($"Attempting edge hit: {hit.point} and{vecHit2Obj}");
                // int layerMask = LayerMask.GetMask ("TargetPlaneTargetable");

                // RaycastHit edgeHit;
                //  if (Physics.Raycast(hit.point, vecHit2Obj, out edgeHit, Mathf.Infinity, layerMask))
                //  {
                //     //print ("Found an object - distance: " + edgeHit.distance);
                //     //print ("Found an object: " + edgeHit.collider.gameObject);
                //     cachedHitPoint = edgeHit.point;

                //     Vector3 manualOffset = vecHit2Obj.normalized * 0.2f;
                //     return edgeHit.point - manualOffset;
                //  }
            // }//TODO: Make this more efficient
            // else if (targetObj.TryGetComponent<MultiplyHandler> (out MultiplyHandler handler))
            // {
            //     cachedHitPoint = hit.point;
            //     return hit.point;
            // }
            // else if(targetObj.GetComponentInParent<MultiplyHandler>())
            // {
            //     cachedHitPoint = hit.point;
            //     return hit.point;
            // }
        }
        return cachedHitPoint;
    }
}
