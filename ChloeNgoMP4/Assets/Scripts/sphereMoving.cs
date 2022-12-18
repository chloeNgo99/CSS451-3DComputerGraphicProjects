using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereMoving : MonoBehaviour
{
     public float limit = 1f;
    public Vector3 moveDirection = Vector3.up;
    public Color color = Color.white;
    public float rotate = 0f;
    public float speed = 0.01f;
    public float max = 5f;
    public float min = 4f;
    private int direction = 1;//move in positive directon

    //public Transform capsule;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   

        Vector3 state = moveDirection * (direction * speed * Time.deltaTime);
        transform.localPosition = transform.localPosition + state;
        float objDir = Vector3.Dot(transform.localPosition, moveDirection);
        
        float angle = Mathf.Acos(Vector3.Dot(Vector3.forward,Vector3.right))* Mathf.Rad2Deg;
        Vector3 axis = Vector3.Cross(Vector3.right,Vector3.up);
        transform.rotation *= Quaternion.AngleAxis(rotate * Time.deltaTime, axis);

        
        int directionState =1;
        float rotationState = 0;
        if(objDir > max){
            directionState = -1;
            rotationState = 45f;
        }else if(objDir < min){
            directionState = 1;
            rotationState = -45f;
        }else{
            directionState = direction;
            rotationState = -45f;
        }
        if(directionState != direction){
            direction = directionState;
            rotate = rotationState;
        }
        

    }
}
