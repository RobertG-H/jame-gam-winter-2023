using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class Multiplizer : MonoBehaviour
{
    
    // [SerializeField] GameObject snail;

    GameObject selectedObj;
    MultiplyHandler selectedHandler;
    int ctr;
    // Start is called before the first frame update
    void Start()
    {

            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Unselect()
    {
        this.selectedObj = null;
        this.selectedHandler = null;
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
            Unselect();
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
