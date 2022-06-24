using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
        //sabit deðerler
    float characterSpeed= 0.01f;
    float standLocation = 2.654f;
    public GameObject WomanModel;
    //deðiþkenler
    float z=9f;
    bool isClick= false;
    bool setStart = false;
    //butona týklanýnca týklandý true olup update'in çalýþmasýna ve animasyon oynatmasýna izin veriyor
    public void Click()
    {
        isClick = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isClick)//butona týklandý ise
        {
            if (!setStart)//baþlangýca alýnmadýysa al
            {
                WomanModel.GetComponent<Animator>().SetBool("IsWalking", true);//animasyona geçmesini saðlar
                z = 9f;
                WomanModel.transform.position = new Vector3(5.057f, 0.275f, z);
                setStart = true;
            }
            if (z > standLocation) //beyaz dairenin üstüne gelene kadar devam et
            {
                WomanModel.transform.position = new Vector3(5.057f, 0.275f, z);
                z -= characterSpeed;
            }
            else //beyaz dairenin üstündeysen durdur bir daha týklamaya izin vermek için deðerini false yap
            {
                WomanModel.GetComponent<Animator>().SetBool("IsWalking", false);
                setStart = false;
                isClick = false;
            }
        }
    }
}
