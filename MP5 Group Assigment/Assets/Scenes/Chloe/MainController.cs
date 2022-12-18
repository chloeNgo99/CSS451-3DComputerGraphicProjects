using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // Start is called before the first frame update
    public MyMesh mesh;
    public AxisFrameBehavior axisFrame;
    public singleSliderValue sliderVal;
    public XformControl xFormControl;
    private int currVal, newval = 2;
    void Start()
    {
        currVal = sliderVal.GetMeshRevValue();
        currVal = 2;
        //Debug.Log("Curr value = " + currVal);
        xFormControl.SetSelectedObject(mesh);
    }

    // Update is called once per frame
    void Update()
    {

        newval = sliderVal.GetMeshRevValue();
        //Debug.Log("Curr value = " + currVal + "new val = " + newval);
        if(currVal != newval){
            
            UpdateMeshValue();
            currVal = newval;
        }
        MouseControls();
    }

    // controls the axis frame and sphere
    void MouseControls() {
        // controls for axis frames
        if (Input.GetKey(KeyCode.LeftControl)) {
            mesh.DisplayVertex();
            mesh.DisplayNormals();
            if (Input.GetMouseButtonDown(0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {

                    // if sphere then sets the selected sphere to show axisFrame
                    if (hit.collider.gameObject.name == "Sphere") {
                        axisFrame.SetSelectedTransform(hit.collider.gameObject);
                    }

                    // Axis Frame directions are set to layer 3
                    if (hit.collider.gameObject.layer == 3) {
                        axisFrame.SelectAnAxis(hit.collider.gameObject);
                    }
                }
                // if nothing is selected, set object to null
                else {
                    axisFrame.SetSelectedTransform(null);
                    axisFrame.SelectAnAxis(null);
                }
                
            }
            else if (Input.GetMouseButton(0) && axisFrame.HasAxisSelected()) {
                axisFrame.DragTranslation();
            }
        }
        if (axisFrame.HasAxisSelected() && !Input.GetMouseButton(0)) {
            axisFrame.ResetColor();
        }
        // hides vertex and line if control is let go and if an object is not selected
        if (Input.GetKeyUp(KeyCode.LeftControl) && !axisFrame.HasObjectSelected()) {
            mesh.HideVertex();
            mesh.HideNormals();
        }
    }
    void UpdateMeshValue(){
        mesh.GetResolutionUpdate(sliderVal.GetMeshRevValue());
    }
}
