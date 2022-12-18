using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondCamera : MonoBehaviour
{
    
    public bool showBallCam;
    public Toggle toggle;
    public GameObject target;
    public GameObject secondCamera;
    public BallBehavior ballScript;
    public ARPlacement placementScript;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(placementScript.spawnedObject != null && ballScript.holdingBall){
        //     Vector3 tablePos = placementScript.spawnedObject.transform.position;
        //     // tablePos[0] = this.transform.position[0];
        //     tablePos[1] = this.transform.position[1];
        //     this.transform.forward = tablePos - transform.position;
        //     // print("lookt at tablePos: " + tablePos);

        // }


        showBallCam = toggle.isOn;
        if(showBallCam && target != null){
            // print("show second camera");
            secondCamera.GetComponent<Camera>().enabled = true;
        }else {
            secondCamera.GetComponent<Camera>().enabled = false;
            // print("show main camera");
        }

        if(target!=null){
            this.transform.position = target.transform.position;
        }
    }
}
