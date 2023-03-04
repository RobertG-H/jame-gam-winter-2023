using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rake : MonoBehaviour
{
    public Transform handle;
    Rigidbody rake_Rigidbody;
    public float rake_Thrust = 20f;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        rake_Rigidbody = GetComponent<Rigidbody>();     
        rake_Rigidbody.AddForceAtPosition(transform.up, handle.transform.position, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        // if ball 
        // Debug.Log($"direction: {direction.normalized}");
        Debug.DrawRay(handle.transform.position, transform.up, Color.yellow, 5);
    }

    // make second collider for the trigger on the end of the rake
    // if object has a certian velocity, then apply the force

}
