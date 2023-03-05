using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class Multiplizer : MonoBehaviour
{
    GameObject selectedObj;
    MultiplyHandler selectedHandler;
    int ctr;
    private void Deselect()
    {
        if (this.selectedHandler != null)
        {
            this.selectedHandler.Deselect();
        }
        this.selectedObj = null;
        this.selectedHandler = null;
    }
    public void RightClick()
    {
        Deselect();
    }
    public void Fire(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }
        Debug.Log($"fire {ctr}");
        ctr++;

        // an object is selected
        if (selectedObj != null)
        {
            Debug.Log("selected version");
            // place object
            this.selectedHandler.MaterializeGhost();
            Deselect();
        }
        // no object is selected
        else {
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward*10, Color.red, 20f);
            if (Physics.Raycast(transform.position, transform.forward, out hit)){
                print("Found an object - distance: " + hit.distance);
                print("Found an object: " + hit.collider.gameObject);
                GameObject targetObj = hit.collider.gameObject;

                if (targetObj.TryGetComponent<MultiplyHandler>(out MultiplyHandler handler))
                {
                    this.selectedObj = targetObj;
                    this.selectedHandler = handler;

                    this.selectedHandler.OnSelect(this);
                    //GameObject ghostObj = Ghost(selectedObj);
                }
            }
        }
        
        
    }
    
}
