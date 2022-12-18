using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class CylinderMesh : MonoBehaviour {

	// Use this for initialization
    // public int revolution = 3;
    int triangles;
    int[] t;

    public Slider resolutionSlider;
    public Slider rotationSlider;
    public Text rot;
    public Text res;

    public Material blackTexture;

    Vector3 originalSpot;


    float height = 3;
    public float cynRot;//angle of rotation
    public int cynRes; //amount of vertexes in each row/col
    public DropDownMenu dropdown;
    private bool showVertex;
    int radius = 1;
    private bool firstTime = true;


    int previousRes;
    Vector3[] vCount;
    Vector3[] nCount;


	void Start () {
        // previousRes = cynRes;
        showVertex = dropdown.showVertex;
        Mesh theMesh = GetComponent<MeshFilter>().mesh;   // get the mesh component
        theMesh.Clear();    // delete whatever is there!!
        // Vector3[] v = new Vector3[revolution * revolution];
        Vector3[] v = new Vector3[cynRes * cynRes];
        int i = 0;
        float range = (float) 2/(cynRes-1);
        //int[] t = new int[8*3];         // Number of triangles: 2x2 mesh and 2x triangles on each mesh-unit
        int square = cynRes-1;
        square = square * square;
        triangles = square * 2;
        t = new int[triangles*3];   
        Color[] newColor = new Color[cynRes * cynRes];
        

        Vector3[] n = new Vector3[cynRes*cynRes];
        Vector2[] uv = new Vector2[cynRes*cynRes]; 


        i = 0;//get vertex
        for(int row = 0; row < cynRes;row++){
            for(int col = 0; col < cynRes;col++){
                

                var radians  = ((cynRot/(cynRes-1)) * Mathf.Deg2Rad) * col;

                
                // print(radians);
                float xDirection = Mathf.Sin(radians);
                float yDirection = Mathf.Cos(radians);
                float y = (height/((cynRes-1))) * row;
                // print("y is: " + y);
                // float z = 0;

                Vector3 direction = new Vector3(xDirection, 0, yDirection);
                
                Vector3 vertexLoc = transform.position + (direction * radius);
                
                vertexLoc[1] = y;
                // print("radians: " + radians + "   vertexLoc: " + vertexLoc);
                // print(vertexLoc);
                v[i] = vertexLoc;
                if(col == 0){
                    originalSpot = v[i];
                }
                i++;

                
            }
        }

        // UV
        i = 0;
        range = (float) 1/(cynRes-1);
        for(float y = 0; y <= 1; y += range){
            for(float x = 0; x <= 1; x+= range){
                //Debug.Log(x + " " + y);
                uv[i] = new Vector2(x,y);
                i++;
            }
        }

        //normal vector
        for(int x = 0; x < n.Length; x++){
            // n[x] = new Vector3(0,1,0);
            n[x] = new Vector3(0,0,1);
        }
        
        
        // First create matrix with fix pivot
        i = 0;
        int[,] matrix = new int[cynRes,cynRes];
        for(int r = cynRes- 1; r >= 0; r--){
            for(int c = 0; c  < cynRes; c++){
                matrix[r,c] = i;
                i++;
            }
        }
        
        i = 0;
        int k = cynRes-1;
        for(int r = cynRes-1; r > 0; r--){
            
            for(int c = 0; c < cynRes-1; c++){
                //first half
                t[i] = matrix[r,c];
                i++;

                t[i] = matrix[r-1,c];    
                i++;

                t[i] = matrix[r-1,c+1];
                i++;
                //second half
                t[i] = matrix[r,c];
                i++;

                t[i] = matrix[r-1,c+1];
                i++;

                t[i] = matrix[r,c+1];
                i++;
            }
        }

        for(int color = 0; color < newColor.Length;color++){
            newColor[color] = Color.clear;
        }

        theMesh.vertices = v; //  new Vector3[3];
        theMesh.triangles = t; //  new int[3];
        theMesh.normals = n;
        theMesh.uv = uv;
        theMesh.uv2 = uv;
        theMesh.colors = newColor;
        
        if(firstTime){
            resolutionSlider.onValueChanged.AddListener (delegate {resolutionSliderChanged ();});
            rotationSlider.onValueChanged.AddListener (delegate {rotationSliderChanged ();});
            InitControllers(v);
            InitNormals(v, n);
            firstTime=false;
        }
        ComputeNormals(v, n, triangles, t);
    }

    

    // Update is called once per frame
    void Update () {
        Start();
        Mesh theMesh = GetComponent<MeshFilter>().mesh;
        Vector3[] v = theMesh.vertices;
        Vector3[] n = theMesh.normals;

        vCount = theMesh.vertices;
        nCount = theMesh.normals;

        // UpdateControllers(v);
        // UpdateNormals(v,n);
        

        theMesh.vertices = v;
        theMesh.normals = n;


        res.text = "Resolution:      " + cynRes;
        rot.text = "Rotation:         " + cynRot;
        if(Input.GetKey(KeyCode.LeftControl) && showVertex){//makes the controllers visible
            transform.GetChild(0).transform.localScale = new Vector3(1,1,1);
            UpdateControllers(v);
            UpdateNormals(v,n);
             
        }else {
            transform.GetChild(0).transform.localScale = new Vector3(0,0,0);
        }

        
	}

    public void resolutionSliderChanged(){
        cynRes = (int)resolutionSlider.value;

    }
    public void rotationSliderChanged(){
        cynRot = (float)rotationSlider.value;
    }
}
