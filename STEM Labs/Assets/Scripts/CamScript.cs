using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public enum MovementMode{
        KeyCam,
        FixedCam,
        MouseCam
    }
    [Header("Cam Movement Opt")]
    public MovementMode movementMode;
    [Header("Cam Movement Stats")]
    [SerializeField] private float camMoveSpd; //Camera Movement Speed
    [SerializeField] private float camRotSpd; //Camera Turning (Rotation) Speed
    [SerializeField] private float scrollBounds; //Pixel Bounds for Edge Scrolling
    
    void Start(){
    }

    void Mode_KeyCam(){ //KEY-BASED MOVEMENT
        Vector3 camDir = new Vector3(); //Camera Position Movement Vector
        camDir += Input.GetAxis("Horizontal") * transform.right; //WS Movement
        camDir += Input.GetAxis("Vertical") * transform.forward; //AD Movement

        transform.position += camDir * camMoveSpd * Time.deltaTime; //Position Movement (WASD)
        transform.eulerAngles += new Vector3(0, Input.GetAxis("Turn") * camRotSpd, 0); //Turn Movement (QE)
        // VCam Body => X/Y Damping (Pos. Drag Delay), VCam Aim => Hor./Ver. Damping (Turn Drag Delay)

        //EDGE SCROLLING
        Vector3 mPos = new Vector3(); mPos = Input.mousePosition; //Mouse Position Tracker
        //if(mPos)
    }

    void Update(){
        if(movementMode == MovementMode.KeyCam){
            Mode_KeyCam();
        }
    }
}
