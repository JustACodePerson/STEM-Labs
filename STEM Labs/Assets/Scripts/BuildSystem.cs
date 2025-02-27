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
    public Transform mainCam; //Access Object the Prefabs Will Become Children Of
    public GameObject prefabToInst; //Access Prefab that Will Be Instantiated

    private void Awake(){
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    public static Vector3 mousePos(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Create Line going thru Cam Center and 2D Mouse Pos on Screen
        
        if( Physics.Raycast( ray, out RaycastHit rcHitObj, LayerMask.GetMask("Object") ) ){ //If the Line Collides with an Object
            //Debug.DrawRay(current.mainCam.position, rcHitObj.point, Color.red, 1, true);
            return rcHitObj.point;// + rcHitObj.normal; //Give Offset Position of Collided Object (Object will be Held There)
        }
        else if( Physics.Raycast( ray, out RaycastHit rcHitGrid, LayerMask.GetMask("Grid") ) ){ //If the Line Collides with the Grid
            return rcHitGrid.point; //Give Collision Point Position (Object will be Held There)
        }
        else{ //If No RayCast Hit Detection
            return new Vector3(0, 100, 0); //Hold Object at Position 0,100,0 - Prevents Object Flashing to Center of Screen Occasionally
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

    public void cloneObject(){
        Vector3 position = snapCoordToGrid(Vector3.zero);
        GameObject obj = Instantiate(prefabToInst, position, Quaternion.identity, parentObject);
        obj.AddComponent<ObjectActive>();
    }
}
