using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyHandler : MonoBehaviour
{
    //bool selectable = true;
    [SerializeField] Material ghostColor;
    // GameObject snail;
    GameObject ghost;
    MeshRenderer ghostRenderer;
    Material originalMaterial;

    GameObject targetPlane;

    Multiplizer multiplizer;
    Camera mainCamera;
    [SerializeField] GameObject TargetPlanePrefab;

    bool selected = false;

    private void Awake ()
    {
        Debug.LogWarning ("TODO MultiplyHandler is fetching main camera from global scene");
        mainCamera = Camera.main;

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

        // apply ghostblue
        ghostRenderer = TryToFindMeshRenderer (ghost);
        ghostRenderer.material = ghostColor;
    }

    private void InitializeTargetPlane ()
    {
        Vector3 vecSnl2Obj = gameObject.transform.position - multiplizer.gameObject.transform.position;
        vecSnl2Obj.Normalize ();
        targetPlane = Instantiate (TargetPlanePrefab, transform.position, Quaternion.FromToRotation (-Vector3.up, vecSnl2Obj));
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
            return Vector3.zero;
        return targetPlane.GetComponent<TargetPlane> ().RaycastToEdge (mainCamera.transform.position, mainCamera.transform.forward);
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

        // revert to original material
        ghostRenderer.material = originalMaterial;

        // dereference the object and let it fledge its wings
        this.ghost = null;
        ghostRenderer = null;
        KillTargetPlane ();
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
        this.multiplizer = multiplizer;
        InitializeGhost();
        selected = true;
    }

    public void Deselect()
    {
        KillGhost ();
        selected = false;
    }
}