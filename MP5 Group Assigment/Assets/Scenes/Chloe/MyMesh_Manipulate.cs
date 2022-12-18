using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour {

    GameObject[] mControllers;

    void InitControllers(Vector3[] v)
    {   
        mControllers = new GameObject[v.Length];
        for (int i = 0; i < v.Length; i++)
        {
            mControllers[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            mControllers[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            mControllers[i].transform.localPosition = v[i];
            mControllers[i].transform.parent = this.transform;
            mControllers[i].SetActive(false);
        }
    }

    void ActivateSphere(){
        //Debug.Log("contrl pressed");
        if(mControllers != null && ctrl){
            for (int i =0; i<mControllers.Length; i++){
                mControllers[i].gameObject.SetActive(true);
            }
        }
    }

    void DeActivateSphere(){
        //Debug.Log("contrl NOT pressed");
        if(mControllers != null && ctrl == false){
            for (int i =0; i<mControllers.Length; i++){
                mControllers[i].gameObject.SetActive(false);
            }
        }
    }

    void DeleteControllers(){
        if(mControllers != null){
            for(int i = 0; i < mControllers.Length; i++){
                Destroy(mControllers[i].gameObject);
            }
        }
    }

    public void DisplayVertex() {
        foreach (GameObject vert in mControllers) {
            vert.SetActive(true);
        }
    }

    public void HideVertex() {
        foreach (GameObject vert in mControllers) {
            vert.SetActive(false);
        }
    }
}
