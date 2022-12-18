using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController2 : MonoBehaviour
{
    public Camera MainCamera = null;
    public TheWorld TheWorld = null;
    

    // Use this for initialization
    void Start() {
        Debug.Assert(MainCamera != null);
        Debug.Assert(TheWorld != null);
    }

    // Update is called once per frame
    void Update() {
        // ProcessMouseEvents();
    }
}
