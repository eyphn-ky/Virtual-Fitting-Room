using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TshirtMenu : MonoBehaviour
{
    bool isOpen = false;
    public void TshirtToogleVisibility(GameObject obj)
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
