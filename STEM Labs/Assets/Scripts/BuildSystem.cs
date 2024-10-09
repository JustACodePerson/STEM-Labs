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
    [SerializeField] TileBase whiteTile;

    PlaceableObject objectToPlace;

    private void Awake(){
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }
}
