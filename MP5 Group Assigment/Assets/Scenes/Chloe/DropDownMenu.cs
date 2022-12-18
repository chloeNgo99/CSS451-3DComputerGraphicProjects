using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropDownMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Dropdown dropdown = null;
    public GameObject Mesh;
    public GameObject Cylinder;
    public bool showVertex;
    void Start()
    {
        //Cylinder.SetActive(false);
        showVertex = false;
        Cylinder.GetComponent<Renderer>().enabled = false;
        Debug.Assert(dropdown != null);
        dropdown.onValueChanged.AddListener(UserSelection);
    }

    // Update is called once per frame
    String[] s = {"Mesh", "Cylinder"};
    void UserSelection(int index)
    {
        if(index == 0){ 
            //Cylinder.SetActive(false);
            Cylinder.GetComponent<Renderer>().enabled = false;
            showVertex = false;
            Mesh.SetActive(true);
        }
        if(index == 1){
            //Cylinder.SetActive(true);
            Cylinder.GetComponent<Renderer>().enabled = true;
            showVertex = true;
            Mesh.SetActive(false);
        }
        //always show dropdown menu headlline
        //dropdown.value = 0;
    }
}
