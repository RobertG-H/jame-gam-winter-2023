using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlane : MonoBehaviour
{
    public Vector3 RaycastToEdge(Vector3 cameraPosition, Vector3 cameraDirection)
    {
        RaycastHit hit;
        Debug.DrawRay(cameraPosition, cameraDirection * 100f, Color.black, 100f);

        if (Physics.Raycast(cameraPosition, cameraDirection, out hit)){
            print("Found an object - distance: " + hit.distance);
            print("Found an object: " + hit.collider.gameObject);
            GameObject targetObj = hit.collider.gameObject;

            
            if (targetObj.tag == "TargetPlaneTag")
            {
                Vector3 vecHit2Obj = transform.position - hit.point;
                Debug.DrawRay(hit.point, vecHit2Obj * 100f, Color.cyan, 100f);
                Debug.Log($"Attempting edge hit: {hit.point} and{vecHit2Obj}");

                RaycastHit edgeHit;
                 if (Physics.Raycast(hit.point, vecHit2Obj, out edgeHit))
                 {
                    print("Found an object - distance: " + edgeHit.distance);
                    print("Found an object: " + edgeHit.collider.gameObject);
                    return edgeHit.point;
                 }
            }
        }
        return Vector3.zero;
    }
}
