using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamScript : MonoBehaviour
{
    [Header("Reference Access")]
    public CinemachineVirtualCamera vCam; //Access Cinemachine Cam
    
    public enum CamView{ //Cam Viewing Mode
        Top,
        Bottom,
        Front,
        Back,
        Left,
        Right,
        Roam
    }
    [Header("Camera Mode")]
    public CamView currentCam = (CamView)0;
    
    [Header("Manipulable Stats")]
    [SerializeField] private float camMoveSpd; //Camera Movement Speed
    [SerializeField] private float camRotSpd; //Camera Turning (Rotation) Speed
    [SerializeField] private float scrollBounds; //Pixel Bounds for Edge Scrolling
    
    private Vector3 camFollowOffset; //Access Cam Offset from Focal Point

    void Start(){
        camFollowOffset = vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

    void camRoam(){
        //KEY-BASED MOVEMENT
        Vector3 camDir = new Vector3(); //Camera Position Movement Vector
        camDir += Input.GetAxis("Horizontal") * transform.right; //WS Movement
        camDir += Input.GetAxis("Vertical") * transform.forward; //AD Movement

        transform.position += camDir * camMoveSpd * Time.deltaTime; //Position Movement (WASD)
        transform.eulerAngles += new Vector3(0, Input.GetAxis("Turn") * camRotSpd, 0); //Turn Movement (QE)
        // VCam Body => X/Y Damping (Pos. Drag Delay), VCam Aim => Hor./Ver. Damping (Turn Drag Delay)

        //EDGE SCROLLING
        Vector3 mPos = new Vector3(); mPos = Input.mousePosition; //Mouse Position Tracker
        //if(mPos)

        if(Input.GetKeyDown(KeyCode.R)){
            camFollowOffset.y *= -1; //NEED TO FIX, REVERSE Y-VALUE OF TRANSPOSER BODY OF VCAM
            Debug.Log("Hey");
        }
    }

    void camFixed(){
        //
    }

    void Update(){
        camRoam();
        /*
        if(currentCam == (CamView)0){
            camRoam();
        }else{
            camFixed();
        }
        */
    }
}
