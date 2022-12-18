using System; // for assert
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class singleSliderValue : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text value;

    
    void Start()
    {
        slider.minValue = 2;
        slider.maxValue = 20;
        UpdateText(slider.value);
        slider.onValueChanged.AddListener(UpdateText);
    }

    // Update is called once per frame
    void UpdateText(float val)
    {
        value.text = slider.value.ToString();
    }

    public int GetMeshRevValue(){
        return (int)slider.value;
    }
}
