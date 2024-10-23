using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    private MeshRenderer mRend;
    //public BuildSystem bSys;
    private void Awake(){
        mRend = this.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        mRend.material = Resources.Load<Material>("Materials/Mat_Active");
    }
    private void OnMouseDown(){
        mRend.material = Resources.Load<Material>("Materials/Mat_Inactive");
        Destroy(this);
    }

    private void Update(){
        if (BuildSystem.current.gridToggle){
            transform.position = BuildSystem.current.snapCoordToGrid(BuildSystem.mousePosGrid());
        }
        else{
            transform.position = BuildSystem.mousePosObj();
        }
    }
}
