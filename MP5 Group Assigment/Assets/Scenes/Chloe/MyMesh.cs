using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyMesh : MonoBehaviour {

	// Use this for initialization
    [SerializeField]int resolution;
    int triangles;
    int[] t;
    public bool ctrl;
    //List<int> t;
    Vector3[] n;
    Vector3[] vertex;
    Vector2[] uv;
    Vector2[] cache;
    public Vector3 translateUV, scaleUV;
    public float rotateUV;
	void Start () {
        Debug.Log("Start");
        translateUV = new Vector3(0,0,0);
        scaleUV = new Vector3(1,1,1);
        rotateUV = 0;
        CreateMesh();
        DeActivateSphere();
    
    }

    void CreateMesh(){
        //Debug.Log("************* RESOLUTION = " + resolution + "**************************************");
        Mesh theMesh = GetComponent<MeshFilter>().mesh;   // get the mesh component
        theMesh.Clear();    // delete whatever is there!!

        // 2x2 mesh needs 3x3 vertices
        //Vector3[] v = new Vector3[resolution * resolution];
        vertex = new Vector3[resolution * resolution];
        
        // Number of triangles: 2x2 mesh and 2x triangles on each mesh-unit
        int square = resolution-1;
        square = square * square;
    
        triangles = square * 2;
       
        t = new int[triangles*3];   
        //t = new List<int>();
        //Debug.Log("squares " + square + " triangles " + triangles + " T[] " + t.Length);
        
       // MUST be the same as number of vertices
        //Vector3[] n = new Vector3[resolution*resolution];
        n = new Vector3[resolution*resolution];
        //Vector2[] uv = new Vector2[9];
        uv = new Vector2[resolution*resolution];
        cache = new Vector2[resolution * resolution];
        //Color[] c = new Color[9];

        float row = 1, col = 1, tempR = -1, tempC = -1;
        int i = 0;
        float range = (float) 2/(resolution-1);
        //v[0] = new Vector3(-1, 0, -1)
        //Debug.Log("RESOLUTION = " + resolution + " ============================================== ");
        //Debug.Log("Range = " + range + " Vertex Lengh = " + vertex.Length);
        if(resolution == 7 || resolution == 13 || resolution == 20){

            for(float r = -1; tempR <= row; r+= range){
                //Debug.Log("Row " + r);

                for(float c = -1; tempC <= col; c+= range){
                    vertex[i] = new Vector3(c,0,r);
                    //Debug.Log("Row = " + r + " Col " + c);
                    
                    // if(c+range <= col){
                    //     //Debug.Log("true -> " + (c+range) + " <= Col 1");
                    // }else{
                    //     Debug.Log("FALSE ->> " + + (c+range) + " <= Col 1");
                    // }
                    i++;
                    tempC = c;
                }
                //Debug.Log("-----------End Row------------");
                tempC=-1;
                tempR = r;
                
            }
        }else{
            for(float r = -1; r <= row; r += range){
                for(float c = -1; c <= col; c += range){
                    vertex[i] = new Vector3(c,0,r);
                    i++;
                }
            }
        }
        

        // for(float c = -1; c <= col; c += range){
        //     Debug.Log("Col " + c);
        //     for(float r = -1; r <= row; r += range){
        //         vertex[i] = new Vector3(r,0,c);
        //         Debug.Log("Row = " + r + " Col " + c);
        //         i++;
        //     }
        // }
        
        // for(float z = -1; z < row; z += range){
        //     Debug.Log("Row " + z);
        //     for(float x = -1; x < col; x += range){
        //         vertex[i] = new Vector3(x,0,z);
        //         Debug.Log("Row = " + z + " Col " + x);
        //         i++;
        //     }
        // }

        
        // UV
        // uv[0] = new Vector2(0, 0);
        //uv[1] = new Vector2(0.5f, 0);

        // i = 0;
        // range = (float) 1/(resolution-1);
        // for(float y = 0; y <= 1; y += range){
        //     for(float x = 0; x <= 1; x+= range){
        //         //Debug.Log(x + " " + y);
        //         uv[i] = new Vector2(x,y);
        //         i++;
        //     }
        // }

        i = 0;
        range = (float) 1 / (resolution-1);
        for(float y = 0; y <= 1; y += range){
            for(float x = 0; x <= 1; x+= range){
                uv[i] = new Vector2(x, y);
                cache[i] = uv[i];
                i++;
            }
        }
        //normal vector
        // n[0] = new Vector3(0, 1, 0);
        //Debug.Log("N: " + n.Length);
        for(int x = 0; x < n.Length; x++){
            n[x] = new Vector3(0,1,0);
        }
        
        // First create matrix with fix pivot
        i = 0;
        int[,] matrix = new int[resolution,resolution];
        for(int r = resolution- 1; r >= 0; r--){
            for(int c = 0; c  < resolution; c++){
                matrix[r,c] = i;
                i++;
            }
        }
        /*
        t[0] = 0; t[1] = 3; t[2] = 4;  // 0th triangle
        t[3] = 0; t[4] = 4; t[5] = 1;  // 1st triangle
        */
        i = 0;
        int k = resolution-1;
        for(int r = resolution-1; r > 0; r--){
            
            for(int c = 0; c < resolution-1; c++){
                //first half
                t[i] = matrix[r,c];
                i++;
                //t.add(matrix[r,c]);

                t[i] = matrix[r-1,c];     
                i++;
                //t.add(matrix[r-1,c]);

                t[i] = matrix[r-1,c+1];
                i++;
                //t.add(matrix[r-1,c+1]);

                // //second half
                t[i] = matrix[r,c];
                i++;
                //t.add(matrix[r,c]);

                t[i] = matrix[r-1,c+1];
                i++;
                //t.add(matrix[r-1,c+1]);

                t[i] = matrix[r,c+1];
                i++;
                //t.add(matrix[r,c+1]);
            }
        }
        Color[] color = new Color[resolution*resolution];
        for(int index = 0; index < color.Length; index++){
            color[index] = Color.clear;
        }

        theMesh.vertices = vertex; //  new Vector3[3];
        theMesh.triangles = t; //  new int[3];
        theMesh.normals = n;
        theMesh.uv = uv;
        theMesh.uv2 = uv;
        theMesh.colors = color;
     
        InitControllers(vertex);
        InitNormals(vertex, n);

        // if(Input.GetKeyUp(KeyCode.F)){
        //         //ctrl = true;
        //     Debug.Log("press from method");
        //     DeActivateSphere();
        // }else if(Input.GetKeyDown(KeyCode.F)){
        //     //ctrl = false;
        //     Debug.Log("not press from method");
        //     ActivateSphere();
            
        // }
        
    }

    // Update is called once per frame
    void Update () {
        
            Mesh theMesh = GetComponent<MeshFilter>().mesh;
            Vector3[] v = theMesh.vertices;
            //Debug.Log("UPDATE V: " + v.Length);
            Vector3[] n = theMesh.normals;
            for (int i = 0; i<mControllers.Length; i++)
            {
                v[i] = mControllers[i].transform.localPosition;
            }

            ComputeNormals(v, n, triangles, t);

            theMesh.vertices = v;
            theMesh.normals = n;
            if(Input.GetKeyDown(KeyCode.LeftControl)){
                ctrl = true;
                //Debug.Log("press from update " + ctrl);
                //ActivateSphere();
            }
            if(Input.GetKeyUp(KeyCode.LeftControl)){
                ctrl = false;
                //Debug.Log("not press from update " + ctrl);
                //DeActivateSphere();
            }
            ActivateSphere();
            ActivateCylinder();
            DeActivateSphere();
            DeActivateCylinder();

            Vector2[] cache = theMesh.uv;
            ComputeUV(ref cache);
            theMesh.uv = cache;
	}

    void ComputeUV(ref Vector2[] cache) {
        float range = (float) 1 / (resolution-1);
        Matrix3x3 uvTRS = Matrix3x3Helpers.CreateTRS(new Vector2(translateUV.x, translateUV.y), rotateUV, new Vector2(scaleUV.x, scaleUV.y));
        int i = 0;
        for(float y = 0; y <= 1; y += range){
            for(float x = 0; x <= 1; x+= range){
                cache[i] = Matrix3x3.MultiplyVector2(uvTRS, this.cache[i]);
                i++;
            }
        }
    }
    public void GetResolutionUpdate(int val){
        
        resolution = val;
       
        DeleteControllers();
        DeleteNormal();
        //Debug.Log("************* Start new Mesh ****************");
        CreateMesh();
    }
}
