using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActive : MonoBehaviour
{
    private MeshRenderer mRend; //Access Renderer
    private Transform tForm; //Access Transform
    private Collider col;
    private void Awake(){ //Initialize Components
        col = GetComponent<Collider>();
        tForm = this.transform.GetChild(0).gameObject.GetComponent<Transform>(); //Access Object's Position/Rotation
        mRend = this.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>(); //Access Object's Renderer
        mRend.enabled = true;
        mRend.material = Resources.Load<Material>("Materials/Mat_Active"); //Change Object to Transparent Color
    }

    private void rotateObject(){ //Rotate Object 90 DEG in a Direction
        if (Input.GetKey(KeyCode.LeftShift)){ 
            if (Input.GetKeyDown(KeyCode.W)) tForm.transform.RotateAround(col.bounds.center,Vector3.right,90); //Shift+W
            if (Input.GetKeyDown(KeyCode.S)) tForm.transform.RotateAround(col.bounds.center,Vector3.right,-90); //Shift+A
            if (Input.GetKeyDown(KeyCode.A)) tForm.transform.RotateAround(col.bounds.center,Vector3.up,-90); //Shift+S
            if (Input.GetKeyDown(KeyCode.D)) tForm.transform.RotateAround(col.bounds.center,Vector3.up,90); //Shift+D
            // *Multiplying by Time.deltaTime Makes the angle change by that amount per second*
        }
    }

    private void Update(){
        rotateObject();
        Vector3 mPos = new Vector3(); mPos = Input.mousePosition;

        if(mPos.y > 100){ //If Mouse is Above Vertical Threshhold
            mRend.enabled = true;

            if(Input.GetMouseButtonDown(0)){ //Left Click
                mRend.material = Resources.Load<Material>("Materials/Mat_Inactive"); //Change Object to Solid Color
                BuildSystem.current.cloneObject(); // Make New Object When First Object is Placed
                Destroy(this); //Destroy Script
            }
            
            if(Input.GetMouseButtonDown(1)){ //Right Click
                Destroy(this.gameObject); //Destroy All
            }

            transform.position = BuildSystem.current.snapCoordToGrid(BuildSystem.mousePos()); //Adjust Position to Grid
        }
        else{
            mRend.enabled = false;
        }
    }
}
