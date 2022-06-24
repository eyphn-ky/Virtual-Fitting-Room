using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SliderSizeScript : MonoBehaviour
{
    [SerializeField] private Slider size;
    [SerializeField] private TMP_InputField sizeText;
    void Start()
    {
        size.onValueChanged.AddListener((v) =>
        {
            sizeText.text = v.ToString("0");
        });
    }

    
}
