using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkirtMenu : MonoBehaviour
{
    bool isOpen = false;
    public void SkirtToogleVisibility(GameObject obj)
    {
        if (!isOpen)
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
