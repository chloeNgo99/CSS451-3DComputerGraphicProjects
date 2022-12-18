using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pivotPrimative : MonoBehaviour
{
    // Start is called before the first frame update
    public float limit = 1f;
    public Vector3 moveDirection = Vector3.up;
    public Color color = Color.white;
    public float rotate = 0f;
    public float speed = 1f;
    public float max = 5f;
    public float min = 4f;
    private int direction = 1;//move in positive directon
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //rotate
        //transform.Rotate(Vector3.up, rotate * Time.deltaTime);
        // if going nevatibe diretion = move * -1 * speed

        //Vector3 eulers = transform.rotation.eulerAngles;
        //this.transform.rotation = Quaternion.Euler(new Vector3(Vector3.up,eulers.y,eulers.z));
        
        
        
        Vector3 state = moveDirection * (direction * speed * Time.deltaTime);
        transform.localPosition = transform.localPosition + state;
        float objDir = Vector3.Dot(transform.localPosition, moveDirection);
        
        transform.rotation *= Quaternion.Euler(rotate * Time.deltaTime,0,0);

        int directionState =1;
        float rotationState = 0;
        if(objDir > max){
            directionState = -1;
            rotationState = -45f;
        }else if(objDir < min){
            directionState = 1;
            rotationState = 45f;
        }else{
            directionState = direction;
            rotationState = 45f;
        }
        if(directionState != direction){
            direction = directionState;
            rotate = rotationState;
        }
    }
}
