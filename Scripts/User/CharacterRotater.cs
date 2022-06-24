using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotater : MonoBehaviour
{
    float rotSpeed = 200;
    bool MouseActive = false;
    public GameObject GameObject;
    void OnMouseOver()//mouse karakterin �zerinde mi ?
    {
        if (MouseActive)//mouse objeye t�kl�yor mu?
        {
            OnMouseDrag();//mouse kayd�r�l�yor mu? o zaman fonksiyonu �al��t�r
        }
    }
    void OnMouseDown()//t�klan�p bas�l� tutuluyorsa de�eri truedur sadece script eklenen karaktere bu �zelli�i verir
    {
        MouseActive = true;
    }
    void OnMouseUp()
    {
        MouseActive = false;
    }

    void OnMouseDrag()
    {
        if (GameObject.GetComponent<Animator>().GetBool("IsWalking") == false)
        { 
            if (Input.GetMouseButton(0))
            {
                float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
                transform.Rotate(Vector3.up, -rotX);
            }
        }
    }
}
