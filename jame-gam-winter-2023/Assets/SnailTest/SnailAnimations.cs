using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
public class SnailAnimations : MonoBehaviour
{
    [SerializeField] RigBuilder rigBuilder;
    [SerializeField] SnailSegmentAimer aimer;
    [SerializeField] SnailSegment head;
    [SerializeField] Rigidbody rb;
    float stickDistance = 0.5f;
    bool isSticking = false;
    RaycastHit hit;


    // Update is called once per frame
    void FixedUpdate()
    {
        if(head.RaycastSegment(out hit, stickDistance))
        {
            head.StickToPosition(hit);
            // rb.velocity = Vector3.zero;
            // rb.angularVelocity = Vector3.zero;
            // if(!isSticking)
            // {
            //     rb.velocity = Vector3.zero;
            //     rb.angularVelocity = Vector3.zero;
            //     isSticking = true;
            //     aimer.SetSource(hit.transform);
            //     rigBuilder.layers[0].active = false;
            //     rigBuilder.layers[1].active = true;
            //     rigBuilder.Build();
            // }
        }
        // else if(isSticking)
        // {
        //     isSticking = false;
        //     aimer.SetSource(null);
        //     rigBuilder.layers[0].active = true;
        //     rigBuilder.layers[1].active = false;
        //     rigBuilder.Build();
        // }
    }
}
