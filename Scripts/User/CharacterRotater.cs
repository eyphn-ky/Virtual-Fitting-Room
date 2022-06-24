using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotater : MonoBehaviour
{
    float rotSpeed = 200;
    bool MouseActive = false;
    public GameObject GameObject;
    void OnMouseOver()//mouse karakterin üzerinde mi ?
    {
        if (MouseActive)//mouse objeye týklýyor mu?
        {
            OnMouseDrag();//mouse kaydýrýlýyor mu? o zaman fonksiyonu çalýþtýr
        }
    }
    void OnMouseDown()//týklanýp basýlý tutuluyorsa deðeri truedur sadece script eklenen karaktere bu özelliði verir
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
