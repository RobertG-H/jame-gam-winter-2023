using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeTransparentWhenClose : MonoBehaviour
{
    [SerializeField] GameObject trackObj;
    [SerializeField] SkinnedMeshRenderer meshRenderer;
    [SerializeField] float transparentDist;


    // Update is called once per frame
    void Update()
    {
        float mag = (transform.position - trackObj.transform.position).magnitude;
        Color newCol = meshRenderer.material.color;
        if(mag < transparentDist)
        {
            float t = mag/transparentDist;
            newCol.a = Mathf.Lerp(0.3f, 1.0f, t);
            meshRenderer.material.color = newCol;
        }
        else
        {
            newCol.a = 1.0f;
            meshRenderer.material.color = newCol;
        }
    }
}
