using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlane : MonoBehaviour
{
    Vector3 cachedHitPoint;
    int layerMask;
    void Awake()
    {
        layerMask = LayerMask.GetMask("Targetable");
    }
    public Vector3 RaycastToEdge(Vector3 cameraPosition, Vector3 cameraDirection, GameObject origObj)
    {
        this.cachedHitPoint = origObj.transform.position;
        
        RaycastHit hit;
        //Debug.DrawRay (cameraPosition, cameraDirection * 100f, Color.black, 5);
        // ray 
        if (Physics.Raycast(cameraPosition, cameraDirection, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 vecHit2Obj = transform.position - hit.point;
            cachedHitPoint = hit.point;
            
            // can go on circle of radius 5
            float maxObjDim = 5f;

            Vector3 vecObj2hit = hit.point - origObj.transform.position;

            // ray hit is beyond max circle around origObj
            if (maxObjDim < vecObj2hit.magnitude)
            {
                return origObj.transform.position + (maxObjDim*vecObj2hit.normalized);
            }
            // ray hit is between max circle perimiter and origObj
            else
            {
                return hit.point;
            }
        }
        // if you hit origObj
        else
        {
            return cachedHitPoint;
        }
    }
}
