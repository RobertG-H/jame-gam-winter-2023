using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplyHandler : MonoBehaviour
{
    //bool selectable = true;
    [SerializeField] Material ghostColor;
    // GameObject snail;
    GameObject ghost;
    Multiplizer multiplizer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 GhostPos()
    {
        // todo
        // will need snail, ghost, and view vector
        Vector3 objSize = this.ghost.transform.localScale;
        
        return this.ghost.transform.position += new Vector3(0.0f,0.5f*objSize[1],0.5f*objSize[2]);
        //return this.ghost.transform.position += new Vector3(0.0f,0.5f,0.5f);
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
