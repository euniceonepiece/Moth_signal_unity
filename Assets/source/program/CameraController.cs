using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Character;
    public float x;
    public float y;
    public float xSpeed = 100;
    public float ySpeed = 100;
    public float distence =10;
    public float disSpeed =200;
    public float minDistence = 1;
    public float maxDistence = 300;
    

    private Quaternion rotationEuler;
    private Vector3 cameraPosition;

 void LateUpdate()

  {
     x+=Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
     y-=Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
        
        if(x>360)
        {
            x-=360;
        }
        else if(x<0)
        {
            x+=360;
        }
     distence -= Input.GetAxis("Mouse ScrollWheel")* disSpeed *Time.deltaTime;    
     distence = Mathf.Clamp(distence,minDistence,maxDistence);
     
     rotationEuler = Quaternion.Euler(y,x,0);
     cameraPosition = rotationEuler * new Vector3(0,1,-distence)+ Character.position;

     transform.rotation = rotationEuler;
     transform.position = cameraPosition;

    }
}
