using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamScript : MonoBehaviour
{
    [Header("Reference Access")]
    [SerializeField] private CinemachineVirtualCamera vCam; //Access Cinemachine Cam
    
    public enum CamView{ //Cam Viewing Mode
        Top,Bottom,Front,Back,Left,Right,Roam  //Mode Options
    }
    [Header("Camera Mode")]
    public CamView currentCam = (CamView)0;
    
    [Header("Manipulable Stats")]
    [SerializeField] private float camMoveSpd; //Camera Movement Speed
    [SerializeField] private float camRotSpd; //Camera Turning (Rotation) Speed
    [SerializeField] private float scrollBounds; //Pixel Bounds for Edge Scrolling
    [SerializeField] private float edgeScrollSpd; //Pixel Bounds for Edge Scrolling
    
    private Vector3 camFollowOffset; //Access Cam Offset from Focal Point

    void Start(){
        //camFollowOffset = vCam.m_Transposer.y;
    }

    void camRoam(){
        //KEY-BASED MOVEMENT
        Vector3 camDir = new Vector3(); //Camera Position Movement Vector
        camDir += Input.GetAxis("Horizontal") * transform.right; //WS Movement
        camDir += Input.GetAxis("Vertical") * transform.forward; //AD Movement

        //EDGE SCROLLING
        Vector3 mPos = new Vector3(); mPos = Input.mousePosition; //Mouse Position Tracker
        if(mPos.x < scrollBounds) camDir -= edgeScrollSpd * transform.right; //Left Bound Movement
        if(mPos.x > Screen.width - scrollBounds) camDir += edgeScrollSpd * transform.right; //Right Bound Movement
        if(mPos.y < scrollBounds) camDir -= edgeScrollSpd * transform.forward; //Bottom Bound Movement
        if(mPos.y > Screen.height - scrollBounds) camDir += edgeScrollSpd * transform.forward; //Top Bound Movement

        transform.position += camDir * camMoveSpd * Time.deltaTime; //Position Movement (WASD)
        transform.eulerAngles += new Vector3(0, Input.GetAxis("Turn") * camRotSpd, 0); //Turn Movement (QE)
        // VCam Body => X/Y Damping (Pos. Drag Delay), VCam Aim => Hor./Ver. Damping (Turn Drag Delay)

        if(Input.GetKeyDown(KeyCode.R)){
            //camFollowOffset.y *= -1; //NEED TO FIX, REVERSE Y-VALUE OF TRANSPOSER BODY OF VCAM
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
