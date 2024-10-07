using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CamScript : MonoBehaviour
{
    [Header("Reference Access")]
    [SerializeField] private CinemachineVirtualCamera vCam; //Access Cinemachine Cam
    Vector3[,] camSettings = new Vector3[,]{};
    public enum CamView{ //Cam Viewing Mode
        //Mode Options
        Roam,
        Top,
        Bottom,
        Back,
        Front,
        Left,
        Right  
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

    void Start(){ //INITIALIZATION
        Vector3[,] camSettings = {
            {new Vector3(0,10,-10), transform.right, transform.forward}, //Roam
            {new Vector3(0,10,0), transform.right, transform.forward}, //Top
            {new Vector3(0,-10,0), transform.right, -transform.forward}, //Bottom
            {new Vector3(0,0,-10), transform.right, transform.up}, //Back
            {new Vector3(0,0,10), -transform.right, transform.up}, //Front
            {new Vector3(-10,0,0), transform.forward, transform.up}, //Left
            {new Vector3(10,0,0), -transform.forward, transform.up}, //Right
        };

        //MANIPUABLE VARIABLE LIMITS
        camMoveSpd = Mathf.Clamp(camMoveSpd, 0, 100); //Bounds Move Speed from 0 to 1
        camRotSpd = Mathf.Clamp(camRotSpd, 0, 100); //Bounds Turn Speed from 0 to 1
        edgeScrollSpd = Mathf.Clamp(edgeScrollSpd, 0, 100); //Bounds Edge Scroll Speed from 0 to 1
        zoomSpd = Mathf.Clamp(zoomSpd, 0, 100); //Bounds Zoom Speed from 0 to 1 

        //DEFAULT SETTING
        currentCam = 0;
        vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0,10,-10); //Set Cam Offset
    }
    void keyMoveScroll(Vector3 ws,Vector3 ad){ //KEY MOVEMENT AND EDGE SCROLLING (HORIZONTAL DIR, VERTICAL DIR)        
        //KEY-BASED MOVEMENT
        Vector3 camDir = new Vector3(); //Camera Position Movement Vector
        camDir += Input.GetAxis("HorMove") * ws; //WS Movement
        camDir += Input.GetAxis("VerMove") * ad; //AD Movement

        //EDGE SCROLLING
        Vector3 mPos = new Vector3(); mPos = Input.mousePosition; //Mouse Position Tracker
        if(mPos.x < scrollBounds) camDir -= (.3f + edgeScrollSpd / 100 * .5f) * ws; //Left Bound Movement
        if(mPos.x > Screen.width - scrollBounds) camDir += (.3f + edgeScrollSpd / 100 * .5f) * ws; //Right Bound Movement
        if(mPos.y < scrollBounds) camDir -= (.3f + edgeScrollSpd / 100 * .5f) * ad; //Bottom Bound Movement
        if(mPos.y > Screen.height - scrollBounds) camDir += (.3f + edgeScrollSpd / 100 * .5f) * ad; //Top Bound Movement

        //CAM POSITIONAL MOVEMENT CHANGES
        transform.position += camDir * (10 + camMoveSpd / 100 * 30) * Time.deltaTime; //Position Movement (WASD)
        // VCam Body => X/Y Damping (Pos. Drag Delay), VCam Aim => Hor./Ver. Damping (Turn Drag Delay)
    }
    void mouseZoom(){ //CAMERA ZOOM
        //CAM ZOOM
        if( Input.GetAxis("ScrollWheel") < 0 ) zoomCurrent += (3 + zoomSpd / 100 * 3); //Zoom Out
        else if( Input.GetAxis("ScrollWheel") > 0 ) zoomCurrent -= (3 + zoomSpd / 100 * 3); //Zoom In
        zoomCurrent = Mathf.Clamp(zoomCurrent, zoomMin, zoomMax); //Keep Zoom In Range
        vCam.m_Lens.FieldOfView = zoomCurrent; //Update 
    }
    void camRoam(){ //ROAMING CAMERA
        if(vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y>0) //IF Cam is Upright
            keyMoveScroll(transform.right,transform.forward); //Key Movement
        else //IF Cam is Flipped
            keyMoveScroll(transform.right,-transform.forward); //INverted Vert Key Movement
        //CAM TURNING CHANGES
        transform.eulerAngles += new Vector3(0, Input.GetAxis("Turn") * (.2f + camRotSpd / 100 * .3f), 0); //Turn Movement (QE)

        //CAM FLIPPING
        if( Input.GetKeyDown(KeyCode.R) ) 
            vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y *= -1; //Flip Cam View (Under/Over)
        
    }
    void camFixed(){ //SIDE CAMERAS
        /*if(currentCam == (CamView)1){ //Top View
            vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0,10,0); //Set Cam Offset
            keyMoveScroll(transform.right,transform.forward);
        } 
        else if (currentCam == (CamView)2){ //Bottom View
            vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0,-10,0); //Set Cam Offset
            keyMoveScroll(transform.right,-transform.forward);
        }
        else if (currentCam == (CamView)3){ //Back View
            vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0,0,-10); //Set Cam Offset
            keyMoveScroll(transform.right,transform.up);
        }
        else if (currentCam == (CamView)4){ //Front View
            vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0,0,10); //Set Cam Offset
            keyMoveScroll(-transform.right,transform.up);
        }
        else if (currentCam == (CamView)5){ //Left View
            vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(-10,0,0); //Set Cam Offset
            keyMoveScroll(-transform.forward,transform.up);
        }
        else if (currentCam == (CamView)6){ //Right View
            vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(10,0,0); //Set Cam Offset
            keyMoveScroll(transform.forward,transform.up);
        }*/
        if((int)currentCam > 0){
            vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = camSettings[(int)currentCam,0];
            keyMoveScroll(camSettings[(int)currentCam,1], camSettings[(int)currentCam,2]);
        }
    }
    void camBounds(){ //CAMERA RANGE CONSTRAINTS
        //transform.position.x = Mathf.Clamp(transform.position.x,-50,50);
        //transform.position.y = Mathf.Clamp(transform.position.y,-10,10);
        //transform.position.z = Mathf.Clamp(transform.position.z,-50,50);
    }

    void Update(){ //UPDATE EVERY FRAME
        if( Input.GetKeyDown(KeyCode.M) ) {
            if( (int)currentCam < 6){
                currentCam ++;
                Debug.Log((int)currentCam);
            } 
            else{ 
                vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = new Vector3(0,10,-10); //Set Cam Offset
                currentCam = 0;
            }
            transform.position = new Vector3(); 
        }

        if( currentCam == (CamView)0 ){
            camRoam();
        } 
        else camFixed();
        mouseZoom();
    }
}
