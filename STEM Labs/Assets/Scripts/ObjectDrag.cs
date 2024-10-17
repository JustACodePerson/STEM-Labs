using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;

    private void OnMouseDown(){
        offset = transform.position - BuildSystem.mousePosition();
    }

    private void OnMouseDrag(){
        Vector3 pos = BuildSystem.mousePosition() + offset;
        transform.position = BuildSystem.current.snapCoordToGrid(pos);
    }
}
