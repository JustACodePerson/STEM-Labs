using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildSystem : MonoBehaviour
{
    //NOTE: Export CAD Files as Fine OBJs, Unzip (Extract Files), and Drag n' Drop in Unity
    public static BuildSystem current;
    private Grid grid; //Acess Grid
    public GridLayout gridLayout; //Access Own-Script
    public bool gridToggle = true;
    public Transform parentObject; //Access Object the Prefabs Will Become Children Of
    public GameObject prefabToInst; //Access Prefab that Will Be Instantiated

    private void Awake(){
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    public static Vector3 mousePosGrid(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit rcHit, LayerMask.GetMask("Grid"))){
            return rcHit.point;
        }
        else{
            return Vector3.zero;
        }
    }

    public static Vector3 mousePosObj(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit rcHit, LayerMask.GetMask("Objetcs"))){
            return rcHit.transform.position+rcHit.normal;
        }
        else{
            return Vector3.zero;
        }
    }

    public Vector3 snapCoordToGrid(Vector3 pos){
        Vector3Int cellPos = gridLayout.WorldToCell(pos);
        pos = grid.GetCellCenterWorld(cellPos);
        return pos;
    }

    public void changeObject(GameObject obj){
        prefabToInst = obj;
    }

    /*public Vector3 snapCoordToObjects(Vector3 pos){
        Vector3Int objPos = gridLayout.WorldToCell(pos);
        return pos;
    }*/

    public void cloneObject(){
        Vector3 position = snapCoordToGrid(Vector3.zero);
        GameObject obj = Instantiate(prefabToInst, position, Quaternion.identity, parentObject);
        obj.AddComponent<ObjectActive>();
    }
}
