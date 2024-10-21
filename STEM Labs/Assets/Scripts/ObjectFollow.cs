using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    private MeshRenderer mRend;
    private void Awake(){
        mRend = this.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        mRend.material = Resources.Load<Material>("Material/Mat_Active");
    }
    private void OnMouseDown(){
        mRend.material = Resources.Load<Material>("Material/Mat_Inactive");
        Destroy(this);
    }

    private void Update(){
        transform.position = BuildSystem.current.snapCoordToGrid(BuildSystem.mousePosition());
    }
}
