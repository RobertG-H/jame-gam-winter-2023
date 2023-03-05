using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyHandler : MonoBehaviour
{
    //bool selectable = true;
    [SerializeField] Material ghostColor;
    // GameObject snail;
    GameObject ghost;
    GameObject targetPlane;

    Multiplizer multiplizer;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject TargetPlanePrefab;

    public Vector3 GhostPos()
    {
        // todo
        // will need snail, ghost, and view vector
        // Vector3 objSize = this.ghost.transform.localScale;


        Vector3 vecSnl2Obj = gameObject.transform.position - multiplizer.gameObject.transform.position;
        vecSnl2Obj.Normalize();
        // Vector3 cameravec = new Vector3(0f,0f,0f);
        // Vector3 newpos = Vector3.ProjectOnPlane(vecSnl2Obj, this.mainCamera.transform.forward);

        
        // return this.ghost.transform.position += new Vector3(0.0f,0.5f*objSize[1],0.5f*objSize[2]);
        //return this.ghost.transform.position += new Vector3(0.0f,0.5f,0.5f);
        // return newpos;

        //Plane MyPlane = new Plane(vecSnl2Obj, gameObject.transform.position);
        targetPlane = Instantiate(TargetPlanePrefab, transform.position, Quaternion.FromToRotation(-Vector3.up,vecSnl2Obj));
        return targetPlane.GetComponent<TargetPlane>().RaycastToEdge(mainCamera.transform.position, mainCamera.transform.forward);
        

        // var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // var hit : RaycastHit;
    
        // if (Physics.Raycast (ray, hit, Mathf.Infinity)) {
        //     transform.position = hit.point;
        // }
   //     return vecSnl2Obj;

    }
    private void SetGhost()
    {
        this.ghost = Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);

        // spawn pos
        // ghostObj.transform.position +=  new Vector3(0.0f,0.5f,0.5f);
        this.ghost.transform.position = GhostPos();

        // remove collisions & gravity
        this.ghost.GetComponent<Rigidbody>().detectCollisions = false;
        this.ghost.GetComponent<Rigidbody>().useGravity = false;

        
        // apply ghostblue
        this.ghost.GetComponent<Renderer>().material = ghostColor;
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
        this.ghost.GetComponent<Renderer>().material = gameObject.GetComponent<MeshRenderer>().material;

        // dereference the object and let it fledge its wings
        this.ghost = null;
    }
    public void KillGhost()
    {
        Destroy(this.ghost);
        this.ghost = null;
    }
    public void OnSelect(Multiplizer multiplizer)
    {
        this.multiplizer = multiplizer;
        SetGhost();
    }
}
