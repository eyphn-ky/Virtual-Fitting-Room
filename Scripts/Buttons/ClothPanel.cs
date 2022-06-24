using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothPanel : MonoBehaviour
{
    bool isOpen=false;
    public void ToogleVisibility(GameObject obj)
    {
        if(!isOpen)
        {
            obj.SetActive(true);
            isOpen = true;
        }
        else
        {
            obj.SetActive(false);
            isOpen = false;
        }
        
    }
    
}
