using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLength : MonoBehaviour
{
    float womanLengthMin = 150f;
    float womanLengthShapeValue = 0f; // 150 = 1 ise 200 = 1.33 olur
    float womanLengthInput = 150f;//150 ile 200 arasýnda olacak
    void Start()
    {
        womanLengthShapeValue = float.Parse(String.Format("{0:0.0000}", 1f + (((womanLengthInput - womanLengthMin) * 100) / womanLengthMin) / 100));
        transform.localScale = new Vector3(womanLengthShapeValue,womanLengthShapeValue,womanLengthShapeValue);//burada veritabanýndan gelen boya göre iþ yapýlacak
    }
}
