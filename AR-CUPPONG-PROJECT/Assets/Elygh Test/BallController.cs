using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public GameObject ballPrefab;
    public GameObject mainCam;
    public bool holdingBall;
    public GameObject curBall;
    public Vector3 ballPosition;
    public SecondCamera camScript;

    private int cupsRemaining = 9;
    public TMPro.TextMeshProUGUI text;
    public TMPro.TextMeshProUGUI winner;
    private bool canSpawn = true;
    
    void Start()
    {
        holdingBall = false;
        curBall = null;
        winner.enabled = false;
    }

    void Update()
    {
        if (cupsRemaining <= 0) {
            winner.enabled = true;
        }
        ballPosition = mainCam.transform.position;
        if (curBall == null) {
            RespawnBall();
        }
        else if (curBall.GetComponent<BallBehavior>().holdingBall) {
            ballPosition += (mainCam.transform.forward * 0.5f);
            ballPosition += (mainCam.transform.up * -0.25f);
            curBall.transform.position = ballPosition;
        }
    }

    // Respawns ball
    void RespawnBall() {
        curBall = Instantiate<GameObject>(ballPrefab, ballPosition, Quaternion.identity);
        curBall.GetComponent<BallBehavior>().camera = mainCam;
        // camScript.target = curBall;
        // camScript.ballScript = curBall.GetComponent<BallBehavior>();
        curBall.GetComponent<BallBehavior>().placementScript = this.GetComponent<ARPlacement>();
        holdingBall = true;
    }

    // updates score
    public void Score() {
        cupsRemaining--;
        text.text = "Cups Remaining: " + cupsRemaining;
    }

}
