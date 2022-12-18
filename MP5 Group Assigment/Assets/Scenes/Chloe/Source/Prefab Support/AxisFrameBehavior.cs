using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisFrameBehavior : MonoBehaviour
{
    GameObject selectedObject;
    Vector3 initMousePosition;
    GameObject currentAxis;
    float speed = 5.0f;

    // select an axis
    public void SelectAnAxis(GameObject axis) {
        ResetColor();
        currentAxis = axis;
        if (currentAxis != null) currentAxis.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void ResetColor() {
        if (currentAxis == null) return;
        switch (currentAxis.name) {
            case "X-Axis":
                this.transform.Find("X-Axis").GetComponent<Renderer>().material.color = Color.red;
                break;
            case "Y-Axis":
                this.transform.Find("Y-Axis").GetComponent<Renderer>().material.color = Color.green;
                break;
            case "Z-Axis":
                this.transform.Find("Z-Axis").GetComponent<Renderer>().material.color = Color.blue;
                break;
        }
    }
    public void SetSelectedTransform(GameObject obj) {
        // reset color of selected sphere and disable the axisframe
        if (selectedObject != null) {
            selectedObject.GetComponent<Renderer>().material.color = Color.white;
            this.gameObject.SetActive(false);
        }
        selectedObject = obj;

        // if obj is not null set color to red and set axis frame to the object
        if (obj != null) {
            this.gameObject.SetActive(true);
            selectedObject.GetComponent<Renderer>().material.color = Color.red;
            this.transform.localPosition = obj.transform.localPosition;
            initMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        }
    }

    public void DragTranslation() {
        currentAxis.GetComponent<Renderer>().material.color = Color.yellow;
        Vector3 delta = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,Camera.main.nearClipPlane)) - initMousePosition;
        initMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        switch (currentAxis.name) {
            case "X-Axis":
                delta = new Vector3(delta.x, 0, 0);
                break;
            case "Y-Axis":
                delta = new Vector3(0, delta.y, 0);
                break;
            case "Z-Axis":
                delta = new Vector3(0, 0, delta.z);
                break;
        }
        TranslateObjects(delta);
        
    }
    private void TranslateObjects(Vector3 delta) {
        this.transform.localPosition += delta * speed;
        selectedObject.transform.localPosition += delta * speed;
    }

    public bool HasObjectSelected() {
        if (selectedObject == null) return false;
        else {
            return true;
        }
    }

    public bool HasAxisSelected() {
        if (currentAxis == null) return false;
        else {
            return true;
        }
    }
}