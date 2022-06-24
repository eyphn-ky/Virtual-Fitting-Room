using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
        //sabit de�erler
    float characterSpeed= 0.01f;
    float standLocation = 2.654f;
    public GameObject WomanModel;
    //de�i�kenler
    float z=9f;
    bool isClick= false;
    bool setStart = false;
    //butona t�klan�nca t�kland� true olup update'in �al��mas�na ve animasyon oynatmas�na izin veriyor
    public void Click()
    {
        isClick = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isClick)//butona t�kland� ise
        {
            if (!setStart)//ba�lang�ca al�nmad�ysa al
            {
                WomanModel.GetComponent<Animator>().SetBool("IsWalking", true);//animasyona ge�mesini sa�lar
                z = 9f;
                WomanModel.transform.position = new Vector3(5.057f, 0.275f, z);
                setStart = true;
            }
            if (z > standLocation) //beyaz dairenin �st�ne gelene kadar devam et
            {
                WomanModel.transform.position = new Vector3(5.057f, 0.275f, z);
                z -= characterSpeed;
            }
            else //beyaz dairenin �st�ndeysen durdur bir daha t�klamaya izin vermek i�in de�erini false yap
            {
                WomanModel.GetComponent<Animator>().SetBool("IsWalking", false);
                setStart = false;
                isClick = false;
            }
        }
    }
}
