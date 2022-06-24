using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DigerSahne : MonoBehaviour
{
    FirebaseManager db = new FirebaseManager();

    
    
    public void DigerSahneGecici()
    {


        db.signOutButton();
    }
}
