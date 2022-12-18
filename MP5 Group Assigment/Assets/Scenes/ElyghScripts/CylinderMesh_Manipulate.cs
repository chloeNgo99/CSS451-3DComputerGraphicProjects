using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CylinderMesh : MonoBehaviour {

    GameObject[] mControllers;

    void InitControllers(Vector3[] v)
    {
        // print("creating new controllers");
        mControllers = new GameObject[v.Length];
        for (int i =0; i<v.Length; i++ )
        {
            mControllers[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            mControllers[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            mControllers[i].transform.position = v[i];
            mControllers[i].transform.parent = this.transform.GetChild(0);


            mControllers[i].GetComponent<Renderer>().material=blackTexture;
        }
        
    }

    void newControllers(Vector3[] v)// same as init but has a delete function
    {
        for(int i= 0; i < mControllers.Length;i++){
            Destroy(mControllers[i]);
        }
        mControllers = new GameObject[v.Length];
        for (int i =0; i<v.Length; i++ )
        {
            mControllers[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            mControllers[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            mControllers[i].transform.position = v[i];
            mControllers[i].transform.parent = this.transform.GetChild(0);

            // print("original spot: " + originalSpot);
            if(v[i][0] == originalSpot[0] && v[i][2] == originalSpot[2]){
                // print("found first col: " + v[i]);
            }else {
                mControllers[i].GetComponent<Renderer>().material=blackTexture;
            }
            
        }
        
    }

    void UpdateControllers(Vector3[] v){
        // print("updating controllers " + "count is: " + mControllers.Length);

        if(v.Length != mControllers.Length){
            newControllers(v);
        }
        for(int i = 0; i < mControllers.Length;i++){
            mControllers[i].transform.position = v[i];
        }
    }
}
