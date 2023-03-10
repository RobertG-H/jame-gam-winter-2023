using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MultiplyHandler : MonoBehaviour
{
    //bool selectable = true;
    // GameObject snail;
    Material targetingMaterial;
    GameObject targetingPlane;
    GameObject ghost;
    MeshRenderer ghostRenderer;
    Material originalMaterial;

    GameObject targetPlane;

    Multiplizer multiplizer;
    Camera mainCamera;
    ITriggerOnMultiply multiplyEvent;
    bool selected = false;
    int originalLayer;
    int targetableLayer;
    private void Awake ()
    {
        Debug.LogWarning ("TODO MultiplyHandler is fetching main camera from global scene");
        mainCamera = Camera.main;

        originalLayer = gameObject.layer;
        targetableLayer = LayerMask.NameToLayer("Targetable");
        // Load targeting objects from resources
        targetingMaterial = Resources.Load("Targeting/TargetingMaterial", typeof(Material)) as Material;
        targetingPlane = Resources.Load("Targeting/TargetingPlane", typeof(GameObject)) as GameObject;

        // Try to find orignal material
        MeshRenderer renderer = TryToFindMeshRenderer (gameObject);
        if (renderer != null)
        {
            originalMaterial = renderer.material;
        }
        else
        {
            Destroy (gameObject);
        }
    }

    MeshRenderer TryToFindMeshRenderer(GameObject objectToSearchOn)
    {
        if (objectToSearchOn.TryGetComponent<MeshRenderer> (out MeshRenderer renderer))
        {
            return renderer;
        }
        else
        {
            MeshRenderer childRenderer = objectToSearchOn.GetComponentInChildren<MeshRenderer> ();
            if (childRenderer != null)
            {
                return childRenderer;
            }
        }
        return null;
    }

    private void InitializeGhost ()
    {
        this.ghost = Instantiate (gameObject, gameObject.transform.position, gameObject.transform.rotation);

        // spawn pos
        InitializeTargetPlane ();
        this.ghost.transform.position = GetGhostPosition ();

        // remove collisions & gravity
        this.ghost.GetComponent<Rigidbody> ().detectCollisions = false;
        this.ghost.GetComponent<Rigidbody> ().useGravity = false;
        this.ghost.GetComponent<Rigidbody>().isKinematic = true;

        // apply ghostblue
        ghostRenderer = TryToFindMeshRenderer (ghost);
        ghostRenderer.material = targetingMaterial;
    }

    private void InitializeTargetPlane ()
    {
        Vector3 vecSnl2Obj = gameObject.transform.position - multiplizer.gameObject.transform.position;
        vecSnl2Obj.Normalize ();
        targetPlane = Instantiate (targetingPlane, transform.position, Quaternion.FromToRotation (-Vector3.up, vecSnl2Obj));
    }

    private void Update ()
    {
        if (!selected)
            return;
        UpdateGhost ();
    }

    void UpdateGhost()
    {
        ghost.transform.position = GetGhostPosition ();
    }



    public Vector3 GetGhostPosition()
    {
        if (targetPlane == null)
            return gameObject.transform.position;
        return targetPlane.GetComponent<TargetPlane> ().RaycastToEdge (mainCamera.transform.position, mainCamera.transform.forward, gameObject);
    }

    public GameObject GetGhost()
    {
        return this.ghost;
    }

    public void MaterializeGhost()
    {
        // add back collisions & gravity
        this.ghost.GetComponent<Rigidbody>().detectCollisions = true;
        this.ghost.GetComponent<Rigidbody>().useGravity = true;
        this.ghost.GetComponent<Rigidbody>().isKinematic = false;

        // revert to original material
        ghostRenderer.material = originalMaterial;

        // dereference the object and let it fledge its wings
        this.ghost = null;
        ghostRenderer = null;
        KillTargetPlane ();

        if (TryGetComponent<ITriggerOnMultiply>(out ITriggerOnMultiply multiplyEvent))
        {
            multiplyEvent.MultiplyEvent();
        }
    }



    void KillGhost()
    {
        Destroy(this.ghost);
        this.ghost = null;
        ghostRenderer = null;
        KillTargetPlane ();
    }

    void KillTargetPlane()
    {
        Destroy (targetPlane);
        targetPlane = null;
    }

    public void OnSelect(Multiplizer multiplizer)
    {
        SetLayerForFamily(targetableLayer);
        this.multiplizer = multiplizer;
        InitializeGhost();
        selected = true;
    }

    public void Deselect()
    {
        SetLayerForFamily(originalLayer);
        KillGhost ();
        selected = false;
    }

    void SetLayerForFamily(int layer)
    {
        gameObject.layer = layer;
        Collider[] children = gameObject.GetComponentsInChildren<Collider>();
        foreach(Collider child in children)
        {
            child.gameObject.layer = layer;
        }
    }
}
