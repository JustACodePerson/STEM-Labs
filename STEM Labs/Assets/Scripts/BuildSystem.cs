using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildSystem : MonoBehaviour
{
    //NOTE: Export CAD Files as Fine OBJs, Unzip (Extract Files), and Drag n' Drop in Unity
    
    public static BuildSystem current;
    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] Tilemap mainTilemap;
    public GameObject prefab1;

    private void Awake(){
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update(){
        if( Input.GetKeyDown(KeyCode.P) ){
            InitObject(prefab1);
        }
    }

    public static Vector3 mousePosition(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit)){
            return raycastHit.point;
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

    public void InitObject(GameObject prefab){
        Vector3 position = snapCoordToGrid(Vector3.zero);
        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        obj.AddComponent<ObjectFollow>();
    }
}
