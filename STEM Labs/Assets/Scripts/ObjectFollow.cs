using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    private MeshRenderer mRend; //Access Renderer
    private Transform tForm;
    private void Awake(){
        tForm = this.transform.GetChild(0).gameObject.GetComponent<Transform>(); //Access Object's Position/Rotation
        mRend = this.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>(); //Access Object's Renderer
        mRend.material = Resources.Load<Material>("Materials/Mat_Active"); //Change Object to Transparent Color
    }

    private void rotateObject(){
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            if (Input.GetKeyDown(KeyCode.W)){
                //tForm.transform.rotate(0,90,0); Rotate Object 90 DEG
            }
            if (Input.GetKeyDown(KeyCode.S)){
            }
            if (Input.GetKeyDown(KeyCode.A)){
            }
            if (Input.GetKeyDown(KeyCode.D)){
            }
        }
    }

    private void Update(){
        rotateObject();

        if (BuildSystem.current.gridToggle){ //Grid Building
            transform.position = BuildSystem.current.snapCoordToGrid(BuildSystem.mousePosGrid()); //Adjust Position to Grid
        }
        else{ //Non-Grid Building
            transform.position = BuildSystem.mousePosObj(); // WIP: ADJUST POSITION RELATIVE TO OTHER PLACED BLOCKS
        }

        if(Input.GetMouseButtonDown(0)){ //Left Click
            mRend.material = Resources.Load<Material>("Materials/Mat_Inactive"); //Change Object to Solid Color
            BuildSystem.current.InstObject(); // Make New Object When First Object is Placed
            Destroy(this); //Destroy Script
        }
        if(Input.GetMouseButtonDown(1)){ //Right Click
            Destroy(this.gameObject); //Destroy All
        } 
    }
}
