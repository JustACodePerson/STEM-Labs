using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    public bool objectIsFollowing;

    private void OnAwake(){
        //Make Cube Semi-Transparent
    }
    private void OnMouseDown(){
        //Make Cube Solid Color
        Destroy(this);
    }

    private void Update(){
        transform.position = BuildSystem.current.snapCoordToGrid(BuildSystem.mousePosition());
    }
}
