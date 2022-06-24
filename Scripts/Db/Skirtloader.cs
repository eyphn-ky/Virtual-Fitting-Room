using Firebase.Extensions;
using Firebase.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Skirtloader : MonoBehaviour
{
    RawImage rawImage2;

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
            rawImage2.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
        // Start is called before the first frame update
        void Start()
        {
        rawImage2 = gameObject.GetComponent<RawImage>();
        storage = FirebaseStorage.DefaultInstance;
        storageReference = storage.GetReferenceFromUrl("gs://unity3d-4b03e.appspot.com/skirts");
        StorageReference image = storageReference.Child("s_red.jpeg");
        print("a");
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
