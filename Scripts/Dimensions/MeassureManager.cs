using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MeassureManager : MonoBehaviour
{
    [Header("Meassure Panel")]
    [SerializeField] TextMeshProUGUI sizeValue;
    [SerializeField] TextMeshProUGUI waistValue;
    [SerializeField] TextMeshProUGUI chestValue;
    [SerializeField] TextMeshProUGUI hipValue;

    [SerializeField] TMP_InputField newSize;
    [SerializeField] TMP_InputField newWaist;
    [SerializeField] TMP_InputField newChest;
    [SerializeField] TMP_InputField newHip;

    public DependencyStatus dependencyStatus;
    FirebaseAuth auth;
    public DatabaseReference reference;

   

    void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;

        
    }
    void Start()
    {
        if (auth.CurrentUser == null)
        {
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            //Database
            reference = FirebaseDatabase.DefaultInstance.RootReference;
            PullData();
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            PullData();
        }
    }

    public void SizeUpdate()
    {
        if (newSize.text != null || newSize.text != "" || newSize.text!= sizeValue.text)
        {
            reference.Child("MeassureData").Child(auth.CurrentUser.UserId).Child("size").SetValueAsync(newSize.text);
        }
    }
    public void WaistUpdate()
    {
        if (newWaist.text != null || newWaist.text != "" || newWaist.text != waistValue.text)
        {
            reference.Child("MeassureData").Child(auth.CurrentUser.UserId).Child("waist").SetValueAsync(newWaist.text);
        }
    }
    public void ChestUpdate()
    {
        if (newChest.text != null || newChest.text != "" || newChest.text != chestValue.text)
        {
            reference.Child("MeassureData").Child(auth.CurrentUser.UserId).Child("chest").SetValueAsync(newChest.text);
        }
    }
    public void HipUpdate()
    {
        if (newHip.text != null || newHip.text != "" || newHip.text != hipValue.text)
        {
            reference.Child("MeassureData").Child(auth.CurrentUser.UserId).Child("hip").SetValueAsync(newHip.text);
        }
    }




    void PullData()
    {
        reference.Child("MeassureData").Child(auth.CurrentUser.UserId).GetValueAsync().ContinueWithOnMainThread(task =>
       {
           if (task.IsFaulted)
           {
               Debug.Log("Database Error");
           }else if (task.IsCompleted) {
               DataSnapshot snapshot = task.Result;
               if(snapshot.GetRawJsonValue() == null)
               {
                   Debug.Log("is Empty");
                   CreatingBlankData();
                   PullData();
               }else
               {
                   Debug.Log(snapshot.GetRawJsonValue());
                   MeassureData data = JsonUtility.FromJson<MeassureData>(snapshot.GetRawJsonValue());
                   sizeValue.text = "Size Value " + data.size;
                   waistValue.text = "Waist Value " + data.waist;
                   chestValue.text = "Chest Value " + data.chest;
                   hipValue.text = "Hip Value " + data.hip;

                   
               }
           }
       });
    }
    
    public void CreatingBlankData()
    {
        MeassureData blankData = new MeassureData
        {
            size = 165,
            waist = 70,
            chest= 80,
            hip = 89,
        };
        
        string blankJson = JsonUtility.ToJson(blankData);
        reference.Child("MeassureData").Child(auth.CurrentUser.UserId).SetRawJsonValueAsync(blankJson);       
    }
}
public class MeassureData
{
    public float size;
    public float waist;
    public float chest;
    public float hip;

}