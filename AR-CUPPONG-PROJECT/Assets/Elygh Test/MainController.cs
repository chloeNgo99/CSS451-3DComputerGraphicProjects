using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for GUI elements: Button, Toggle

public partial class MainController : MonoBehaviour {

    // reference to all UI elements in the Canvas
    public Camera MainCamera = null;
    public TheWorld TheWorld = null;
    public SceneNodeControl NodeControl = null;

    public string tableLevelName = null, currentLevel = null;
    
    void Awake()
    {
        Debug.Assert(NodeControl != null);
        NodeControl.TheRoot = TheWorld.TheRoot;
        currentLevel = TheWorld.getLevel();
    }

    // Use this for initialization
    void Start() {
        Debug.Assert(MainCamera != null);
        Debug.Assert(TheWorld != null);
    }

    // Update is called once per frame
    void Update() {
        // ProcessMouseEvents();
        // tableLevelName = TheWorld.getLevel();
        // if(currentLevel != tableLevelName){
           
        //     currentLevel = tableLevelName;
        //     Debug.Log("from main control " + currentLevel );

        //     NodeControl.TheRoot.transform.name = currentLevel;
        // }
    }
}