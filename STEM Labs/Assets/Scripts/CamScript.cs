using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamScript : MonoBehaviour
{
    [Header("Reference Access")]
    [SerializeField] private CinemachineVirtualCamera vCam; //Access Cinemachine Cam
    
    public enum CamView{ //Cam Viewing Mode
        Roam,Top,Bottom,Front,Back,Left,Right  //Mode Options
    }

    [Header("Camera Mode")]
    public CamView currentCam = (CamView)0; //Default Cam View
    
    [Header("Directional Movement")]
    [SerializeField] private float camMoveSpd = 66f; //Default Camera Movement Speed (0-100) ; 0 (10) , 1 (40)
    [SerializeField] private float camRotSpd = 33f; //Default Camera Turning (Rotation) Speed (0-100) ; 0 (.2) , 1 (.5)

    [Header("Edge Scrolling")]
    [SerializeField] private float scrollBounds = 15; //Pixel Bounds for Edge Scrolling
    [SerializeField] private float edgeScrollSpd = 20f; //Default Edge Scrolling Speed (0-100) ; 0 (.3) , 1 (.8) 

    [Header("Zoom")]
    [SerializeField] private float zoomCurrent = 60; //Current Zoom Value, Default at 60
    [SerializeField] private float zoomMin = 80; //Zoom Out Max
    [SerializeField] private float zoomMax = 20; //Zoom In Max
    [SerializeField] private float zoomSpd = 0; //Default Speed of Zoom (0-100) ; 0 (3) , 1 (6)

    void Start(){}

    void camRoam(){
        //MANIPUABLE VARIABLE LIMITS
        camMoveSpd = Mathf.Clamp(camMoveSpd, 0, 100); //Bounds Move Speed from 0 to 1
        camRotSpd = Mathf.Clamp(camRotSpd, 0, 100); //Bounds Turn Speed from 0 to 1
        edgeScrollSpd = Mathf.Clamp(edgeScrollSpd, 0, 100); //Bounds Edge Scroll Speed from 0 to 1
        zoomSpd = Mathf.Clamp(zoomSpd, 0, 100); //Bounds Zoom Speed from 0 to 1

        //KEY-BASED MOVEMENT
        Vector3 camDir = new Vector3(); //Camera Position Movement Vector
        camDir += Input.GetAxis("HorMove") * transform.right; //WS Movement
        camDir += Input.GetAxis("VerMove") * transform.forward; //AD Movement

        //EDGE SCROLLING
        Vector3 mPos = new Vector3(); mPos = Input.mousePosition; //Mouse Position Tracker
        if(mPos.x < scrollBounds) camDir -= (.3f + edgeScrollSpd / 100 * .5f) * transform.right; //Left Bound Movement
        if(mPos.x > Screen.width - scrollBounds) camDir += (.3f + edgeScrollSpd / 100 * .5f) * transform.right; //Right Bound Movement
        if(mPos.y < scrollBounds) camDir -= (.3f + edgeScrollSpd / 100 * .5f) * transform.forward; //Bottom Bound Movement
        if(mPos.y > Screen.height - scrollBounds) camDir += (.3f + edgeScrollSpd / 100 * .5f) * transform.forward; //Top Bound Movement

        //CAM MOVEMENT CHANGES
        transform.position += camDir * (10 + camMoveSpd / 100 * 30) * Time.deltaTime; //Position Movement (WASD)
        transform.eulerAngles += new Vector3(0, Input.GetAxis("Turn") * (.2f + camRotSpd / 100 * .3f), 0); //Turn Movement (QE)
        // VCam Body => X/Y Damping (Pos. Drag Delay), VCam Aim => Hor./Ver. Damping (Turn Drag Delay)

        
        //CAM FLIPPING
        if( Input.GetKeyDown(KeyCode.R) ) 
            vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y *= -1; //Flip Cam View (Under/Over)

        //CAM ZOOM
        if( Input.GetAxis("ScrollWheel") < 0 ) zoomCurrent += (3 + zoomSpd / 100 * 3); //Zoom Out
        else if( Input.GetAxis("ScrollWheel") > 0 ) zoomCurrent -= (3 + zoomSpd / 100 * 3); //Zoom In
        zoomCurrent = Mathf.Clamp(zoomCurrent, zoomMin, zoomMax); //Keep Zoom In Range
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
