using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class Multiplizer : MonoBehaviour
{
    GameObject selectedObj;
    MultiplyHandler selectedHandler;
    [SerializeField] Transform aimingTransform;
    int ignoreMask;
    void Awake()
    {
        int mask = 1 << LayerMask.NameToLayer("Player");
        mask += 1 << LayerMask.NameToLayer("Level");
        ignoreMask = ~mask;
    }

    public void RightClick()
    {
        Deselect();
    }

    public void Fire()
    {

        // an object is selected
        if (selectedObj != null)
        {
            Debug.Log($"Placing object {selectedObj.name}");
            // place object
            this.selectedHandler.MaterializeGhost();
            Deselect();
        }
        // no object is selected
        else {
            RaycastHit hit;
            Debug.DrawRay(aimingTransform.position, aimingTransform.forward*10, Color.red, 20f);
            if (Physics.Raycast(aimingTransform.position, aimingTransform.forward, out hit, Mathf.Infinity, ignoreMask)){
                Debug.Log($"Found an object: {hit.collider.gameObject.name} at dist {hit.distance}");
                GameObject targetObj = hit.collider.gameObject;

                // Get on object
                if (targetObj.TryGetComponent<MultiplyHandler>(out MultiplyHandler handler))
                {
                    Select(handler, targetObj);
                }
                else
                {
                    // If doesn't exist, get in parent
                    MultiplyHandler parentHandler = targetObj.GetComponentInParent<MultiplyHandler>();
                    if(parentHandler != null)
                        Select(parentHandler, parentHandler.gameObject);
                }
            }
        }        
    }

    void Select(MultiplyHandler handler, GameObject targetObj)
    {
        this.selectedObj = targetObj;
        this.selectedHandler = handler;
        this.selectedHandler.OnSelect(this);
    }

    void Deselect ()
    {
        if (this.selectedHandler != null)
        {
            this.selectedHandler.Deselect ();
        }
        this.selectedObj = null;
        this.selectedHandler = null;
    }
}
