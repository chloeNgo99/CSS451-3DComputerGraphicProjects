using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    private Rigidbody rb;
    public bool holdingBall = true;
    Vector3 startPosition;
    Vector3 endPosition;
    float timeElapsed;
    float touchTimeStart, touchTimeEnd, timeInterval;
    public GameObject camera;
    private bool ignore;
    public float speed = 10.0f;
    public ARPlacement placementScript;
    BallController ballController;
    void Start()
    {
        ballController = FindObjectOfType<BallController>();
        rb = this.GetComponent<Rigidbody>();
        // disable gravity until clicked
        rb.useGravity = false;
        ignore = false; 
    }

    
    void FixedUpdate()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && holdingBall) {
            ignore = false;
            touchTimeStart = Time.time;
            startPosition = Input.GetTouch(0).position;
            if(startPosition[0] < 300f || startPosition[0] > 550f || startPosition[1] > 250f || placementScript.spawnedObject == null){
                ignore = true;
            }
            print(Input.GetTouch(0).position);
        }

        // release ball when mouse let go
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && holdingBall && !ignore) {
            touchTimeEnd = Time.time;
            timeInterval = touchTimeEnd - touchTimeStart;
            endPosition = Input.GetTouch(0).position;
            holdingBall = false;
            rb.useGravity = true;

            Vector3 force = endPosition - startPosition;
            force.z = force.y;
            rb.AddForce(camera.transform.right * force.x * speed, ForceMode.Force);
            rb.AddForce(camera.transform.up * force.y * speed, ForceMode.Force);
            rb.AddForce(camera.transform.forward * force.z * speed, ForceMode.Force);
            ballController.curBall = null;




            // Vector3 direction = camera.transform.forward;
            // direction *= (endPosition-startPosition).y;

            // rb.AddForce(force *15f, ForceMode.Force);

        }
        if (!holdingBall) Destroy(this.gameObject, 1.0f);
    }
}
