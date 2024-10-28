using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    private MeshRenderer mRend; //Access Renderer
    private void Awake(){
        mRend = this.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>(); //Access Object Color in Renderer
        mRend.material = Resources.Load<Material>("Materials/Mat_Active"); //Change Object to Transparent Color
    }

    private void Update(){
        if (BuildSystem.current.gridToggle){ //Grid Building
            transform.position = BuildSystem.current.snapCoordToGrid(BuildSystem.mousePosGrid()); //Adjust Position to Grid
        }
        else{ //Non-Grid Building
            transform.position = BuildSystem.mousePosObj(); // WIP: Adjust Position to Block
        }

        if(Input.GetMouseButtonDown(0)){ //Left Click
            mRend.material = Resources.Load<Material>("Materials/Mat_Inactive"); //Change Object to Solid Color
            //BuildSystem.InstObject(); // WIP: Make New Object
            Destroy(this); //Destroy Script
        }
        if(Input.GetMouseButtonDown(1)){ //Right Click
            Destroy(this.gameObject); //Destroy All
        } 
    }
}
