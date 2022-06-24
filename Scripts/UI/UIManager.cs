using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject meassureUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance var, nesneyi sil");
            Destroy(this);
        }
    }
    public void ClearScreen()
    {
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        meassureUI.SetActive(false);
    }

    
    public void LoginScreen() 
    {
        ClearScreen();
        loginUI.SetActive(true);
        
    }
    public void RegisterScreen() 
    {
        ClearScreen();
        registerUI.SetActive(true);
        
    }
    public void MeassureScreen()
    {
        ClearScreen();
        meassureUI.SetActive(true);

    }
}
