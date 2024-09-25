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
    [SerializeField] private float camMoveSpd 25; //Camera Movement Speed
    [SerializeField] private float camRotSpd = .3; //Camera Turning (Rotation) Speed
    [SerializeField] private float scrollBounds = 15; //Pixel Bounds for Edge Scrolling
    [SerializeField] private float edgeScrollSpd = .5; //Pixel Bounds for Edge Scrolling
    [SerializeField] private float zoomCurrent = 60; //Current Zoom Value, Default at 60
    [SerializeField] private float zoomMin = 80; //Zoom Out Max
    [SerializeField] private float zoomMax = 20; //Zoom In Max

    void Start(){
    }

    void camRoam(){
        //KEY-BASED MOVEMENT
        Vector3 camDir = new Vector3(); //Camera Position Movement Vector
        camDir += Input.GetAxis("HorMove") * transform.right; //WS Movement
        camDir += Input.GetAxis("VerMove") * transform.forward; //AD Movement

        //EDGE SCROLLING
        Vector3 mPos = new Vector3(); mPos = Input.mousePosition; //Mouse Position Tracker
        if(mPos.x < scrollBounds) camDir -= edgeScrollSpd * transform.right; //Left Bound Movement
        if(mPos.x > Screen.width - scrollBounds) camDir += edgeScrollSpd * transform.right; //Right Bound Movement
        if(mPos.y < scrollBounds) camDir -= edgeScrollSpd * transform.forward; //Bottom Bound Movement
        if(mPos.y > Screen.height - scrollBounds) camDir += edgeScrollSpd * transform.forward; //Top Bound Movement

        //CAM MOVEMENT CHANGES
        transform.position += camDir * camMoveSpd * Time.deltaTime; //Position Movement (WASD)
        transform.eulerAngles += new Vector3(0, Input.GetAxis("Turn") * camRotSpd, 0); //Turn Movement (QE)
        // VCam Body => X/Y Damping (Pos. Drag Delay), VCam Aim => Hor./Ver. Damping (Turn Drag Delay)

        //CAM FLIPPING
        if(Input.GetKeyDown(KeyCode.R))
            vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y *= -1; //Flip Cam View (Under/Over)

        //CAM ZOOM
        if(Input.GetAxis("ScrollWheel") > 0) zoomCurrent += .5; //Zoom Out
        else if(Input.GetAxis("ScrollWheel") < 0) zoomCurrent -= .5; //Zoom In
        Mathf.Clamp(zoomCurrent, zoomMin, zoomMax); //Keep Zoom In Range
        vCam.m_Lens.FieldOfView = zoomCurrent; //Update 
    }

    void camFixed(){
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
