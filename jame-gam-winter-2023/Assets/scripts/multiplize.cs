using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multiplize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward*10, Color.red, 20f);
        if (Physics.Raycast(transform.position, transform.forward, out hit)){
            print("Found an object - distance: " + hit.distance);
            print("Found an object: " + hit.collider.gameObject);
            GameObject obj = hit.collider.gameObject;
            Instantiate(obj, obj.transform.position + new Vector3(0.5f,0.0f,0.0f), obj.transform.rotation);

        }

            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
