using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    public BallController ballController;
    Vector3 distance;
    void Start()
    {
        ballController = FindObjectOfType<BallController>();
    }

    
    // void Update()
    // {
    //     if (ballController.curBall != null) {
    //         distance = this.transform.position - ballController.curBall.transform.position;
    //         if (distance.magnitude <= this.transform.lossyScale.x) {
                
    //             ballController.Score();
                
    //             Destroy(this.gameObject);
    //         }
    //     }
    // }

    // checks for collision with ball, deletes both GameObjects
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "ball") {
            ballController.Score();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
