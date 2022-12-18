﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour {
    LineSegment[] mNormals;

    void InitNormals(Vector3[] v, Vector3[] n)
    {
        mNormals = new LineSegment[v.Length];
        for (int i = 0; i < v.Length; i++)
        {
            GameObject o = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            mNormals[i] = o.AddComponent<LineSegment>();
            mNormals[i].SetWidth(0.02f);
            mNormals[i].transform.SetParent(this.transform);
            mNormals[i].gameObject.SetActive(false);
        }
        UpdateNormals(v, n);
    }

    void ActivateCylinder(){
        //Debug.Log("contrl pressed");
        if(mNormals != null && ctrl){
            for (int i =0; i<mNormals.Length; i++){
                mNormals[i].gameObject.SetActive(true);
            }
        }
    }

    void DeActivateCylinder(){
        //Debug.Log("contrl NOT pressed");
        if(mNormals != null && ctrl == false){
            for (int i =0; i<mNormals.Length; i++){
                mNormals[i].gameObject.SetActive(false);
            }
        }
    }

    void DeleteNormal(){
        if(mNormals != null){
            for(int i = 0; i < mNormals.Length; i++){
                Destroy(mNormals[i].gameObject);
            }
        }
    }

    void UpdateNormals(Vector3[] v, Vector3[] n)
    {
        //Debug.Log("UPDATE NORMAL ---------- ");
        for (int i = 0; i < v.Length; i++)
        {
            mNormals[i].SetEndPoints(v[i], v[i] + 1.0f * n[i]);
            // Debug.Log("mNormals[" +i+ "] = " + mNormals[i] + "V[I] = " + v[i] + "+ 1 *" + "N[I] = " + n[i] );
            // Debug.Log("mNormals[" +i+ "] = " + mNormals[i] + "P1 = " + v[i] + "P2 = " + (v[i] + 1 * n[i]) );
        }
    }

    Vector3 FaceNormal(Vector3[] v, int i0, int i1, int i2)
    {
        Vector3 a = v[i1] - v[i0];
        //Debug.Log("V[I0] = " + v[i0] + " V[I1] = " + v[i1] + "V [I2] = " + v[i2]);
        Vector3 b = v[i2] - v[i0];
        //Debug.Log("CROSS PRODUCT = " + Vector3.Cross(a, b).normalized + " A = " + a + " B = " + b);
        return Vector3.Cross(a, b).normalized;
        //return Vector3.Cross(b, a).normalized;
    }

    void ComputeNormals(Vector3[] v, Vector3[] n, int num, int[] t)
    {
        //Debug.Log("NUMBER: " + num + " T : " + t.Length);
        //Debug.Log("=============== T = " +t.Length + " ===================");
        /*
        t[0] = 0; t[1] = 3; t[2] = 4;  // 0th triangle
        t[3] = 0; t[4] = 4; t[5] = 1;  // 1st triangle

        t[6] = 1; t[7] = 4; t[8] = 5;  // 2nd triangle
        t[9] = 1; t[10] = 5; t[11] = 2;  // 3rd triangle

        t[12] = 3; t[13] = 6; t[14] = 7;  // 4th triangle
        t[15] = 3; t[16] = 7; t[17] = 4;  // 5th triangle

        t[18] = 4; t[19] = 7; t[20] = 8;  // 6th triangle
        t[21] = 4; t[22] = 8; t[23] = 5;  // 7th triangle
        */
        Vector3[] triNormal = new Vector3[num];
        List<List<int>> list = new List<List<int>>();
        int x = 0;
        List<int> temp = new List<int>(); 
        for(int i = 0; i < t.Length; i += 3){
            triNormal[x] = FaceNormal(v,t[i+1], t[i+2], t[i]);          
            //Debug.Log("I = " + i + " X = "+ x + " -> " + t[i+1] +" "+ t[i+2] + " "+ t[i]);  
            temp.Add(t[i]);
            temp.Add(t[i+1]);
            temp.Add(t[i+2]);
            list.Add(temp);
            x++;
            i+=3;

            triNormal[x] = FaceNormal(v,t[i], t[i+1], t[i+2]);
            //Debug.Log("I = " + i + " X = "+ x + " -> " + t[i] +" "+ t[i+1] + " "+ t[i+2]); 
            temp = new List<int>();
            temp.Add(t[i]);
            temp.Add(t[i+1]);
            temp.Add(t[i+2]);
            list.Add(temp);
            x++;
            temp = new List<int>(); 
        }

        /*
        triNormal[0] = FaceNormal(v, 3, 4, 0);
        triNormal[1] = FaceNormal(v, 0, 4, 1);

        triNormal[2] = FaceNormal(v, 4, 5, 1);
        triNormal[3] = FaceNormal(v, 1, 5, 2);

        triNormal[4] = FaceNormal(v, 6, 7, 3);
        triNormal[5] = FaceNormal(v, 3, 7, 4);

        triNormal[6] = FaceNormal(v, 7, 8, 4);
        triNormal[7] = FaceNormal(v, 4, 8, 5);
        */

        //Debug.Log("List count" + list.Count);
        // for(int j = 0; j < list.Count; j++){
        //     List<int> temp = list[j];
        //     for(int i = 0; i < temp.Count; i++){
        //         Debug.Log(temp[i]);
        //     }
        //     Debug.Log("----------------");
        // }
        //Debug.Log("LIST COUNT : "+list.Count);
        for(int i = 0; i < n.Length; i++){
            Vector3 norm = new Vector3();
            //Debug.Log("i : " + i);
            //Debug.Log("N[I] At I = " + i + " --------");
            for(int j = 0; j < list.Count; j++){ 
                 List<int> curr = list[j];
                //  for(int k = 0; k < temp.Count; k++){
                //      Debug.Log(temp[k]);
                //  }
                // Debug.Log("END LIST ----------");
                if(curr.Contains(i)){
                    
                    for(int k = 0; k < curr.Count; k++){
                        //Debug.Log(temp[k]);
                    }
                    norm += triNormal[j];
                }
            }
            //Debug.Log("Norm = "+norm + "Normomarlize = " + norm.normalized);
            //Debug.Log("END LIST ----------");
             
            n[i] = norm.normalized;
        }

        
        // n[0] = (triNormal[0] + triNormal[1]).normalized;
        // Debug.Log("N[0] - "+n[0]);

        // n[1] = (triNormal[1] + triNormal[2] + triNormal[3]).normalized;
        // Debug.Log("N[1] - "+n[1]);

        // n[2] = triNormal[3].normalized;
        // Debug.Log("N[2] - "+n[2]);

        // n[3] = (triNormal[0] + triNormal[4] + triNormal[5]).normalized;
        // Debug.Log("N[3] - "+n[3]);

        // n[4] = (triNormal[0] + triNormal[1] + triNormal[2] + triNormal[5] + triNormal[6] + triNormal[7]).normalized;
        // Debug.Log("N[4] - "+n[4]);

        // n[5] = (triNormal[2] + triNormal[3]).normalized;
        // Debug.Log("N[5] - "+n[5]);

        // n[6] = triNormal[4].normalized;
        // Debug.Log("N[6] - "+n[6]);

        // n[7] = (triNormal[4] + triNormal[5] + triNormal[6]).normalized;
        // Debug.Log("N[7] - "+n[7]);

        // n[8] = (triNormal[6] + triNormal[7]).normalized;
        // Debug.Log("N[8] - "+n[8]);

        UpdateNormals(v, n);
    }

    public void DisplayNormals() {
        foreach (LineSegment normal in mNormals) {
            normal.gameObject.SetActive(true);
        }
    }
    public void HideNormals() {
        foreach (LineSegment normal in mNormals) {
            normal.gameObject.SetActive(false);
        }
    }
}
