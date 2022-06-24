using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Firebase;
using Firebase.Extensions;
using Firebase.Storage;
using System;
using System.Threading.Tasks;

public class TshirtLoader : MonoBehaviour
{
    RawImage rawImage1;
    FirebaseStorage storage;

    StorageReference storageReference;

    [Obsolete]
    IEnumerator LoadImage(string MediaUrl)
    {
      
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            rawImage1.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }

            
    }
    // Start is called before the first frame update
    void Start()
    {  
        rawImage1 = gameObject.GetComponent<RawImage>();
        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://unity3d-4b03e.appspot.com/tshirts/");
        StorageReference image = storageReference.Child("ts_pink.png");
        print("x");
        image.GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsFaulted && !task.IsCanceled)
            {
             
                StartCoroutine(LoadImage(Convert.ToString(task.Result)));
            }
            else
            {
                Debug.Log(task.Exception);
            }
        });

    }

}
