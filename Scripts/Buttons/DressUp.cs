using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DressUp : MonoBehaviour
{
    public RawImage rawImage1;
    public RawImage rawImage2;

    bool isActive = false;
    public void Dress(GameObject dress)
    {
        if(isActive)
        {
            dress.SetActive(false);
            isActive = false;
        }
        else
        {
            dress.SetActive(true);
            isActive = true;
        }
    }


        
}
