using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class Multiplizer : MonoBehaviour
{
    GameObject selectedObj;
    MultiplyHandler selectedHandler;
    [SerializeField] Transform aimingTransform;
    [SerializeField] GameObject multiplyParticles;
    [SerializeField] AudioEventChannelSO audioEventChannelSO;
    [SerializeField] AudioClipSO placeAudio;
    [SerializeField] AudioClipSO selectAudio;

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
            PlayParticles();
            audioEventChannelSO.RaiseEvent(placeAudio, transform.position);
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
        audioEventChannelSO.RaiseEvent(selectAudio, transform.position);
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

    void PlayParticles()
    {
        GameObject instance = Instantiate(multiplyParticles, this.selectedHandler.GetGhost().transform.position, this.selectedHandler.GetGhost().transform.rotation, this.selectedHandler.GetGhost().transform);
        var ps = instance.GetComponent<ParticleSystem>();
        var shape = ps.shape;

        MeshFilter objectMeshFilter = selectedHandler.GetComponent<MeshFilter>();
        if(objectMeshFilter == null)
            objectMeshFilter = selectedHandler.GetComponentInChildren<MeshFilter>();
        if(objectMeshFilter != null)
        {
            shape.mesh = objectMeshFilter.mesh;
            shape.scale = this.selectedHandler.GetGhost().transform.localScale;
        }

    }
}
