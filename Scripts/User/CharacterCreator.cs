using UnityEngine;
using System.Collections;

public class CharacterCreator : MonoBehaviour
{

    SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh skinnedMesh;
    float willAddMeasurementRatio = 0f;

    float womanWaistCmMin = 53f; //0. indis
    float womanWaistCmMax = 82.15f;
    float womanWaistInput = 70f;//veritaban�ndan al�nacak
    float womanWaistShapeValue = 0f;

    float womanChestCmMin = 76f;// 1. indis
    float womanChestCmMax = 102f;
    float womanChestInput = 80f;//veritaban�ndan al�nacak
    float womanChestShapeValue = 0f; //bra i�inde ayn� shape verilecek

    float womanHipCmMin = 75f; //2. indis
    float womanHipCmMax = 107f;
    float womanHipInput = 85f;//veritaban�ndan al�nacak
    float womanHipShapeValue = 0f;//underwear i�inde ayn� shape verilecek

    //di�er shapelerin ortalamas� verilecek
    //3.indis kalf
    //4.indis kollar
    //5.indis y�z
    //6.indis bra
    //7.indis underwear
    void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        skinnedMesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
    }

    void Start()
    {
        willAddMeasurementRatio = ((float.Parse(GameObject.Find("WomanModel").transform.localScale.x.ToString()) % 1) * 100);//WomanModel objesinin scale edildiginde de�erini al�yor.
        womanWaistCmMin = womanWaistCmMin + (womanWaistCmMin * willAddMeasurementRatio / 100);
        womanWaistCmMax = womanWaistCmMax + (womanWaistCmMax * willAddMeasurementRatio / 100);
        womanWaistShapeValue = ((womanWaistInput - womanWaistCmMin) * 100) / (womanWaistCmMax - womanWaistCmMin);
        skinnedMeshRenderer.SetBlendShapeWeight(0, womanWaistShapeValue);

        womanChestCmMin = womanChestCmMin + (womanChestCmMin * willAddMeasurementRatio / 100);
        womanChestCmMax = womanChestCmMax + (womanChestCmMax * willAddMeasurementRatio / 100);
        womanChestShapeValue = ((womanChestInput - womanChestCmMin) * 100) / (womanChestCmMax - womanChestCmMin);
        skinnedMeshRenderer.SetBlendShapeWeight(1, womanChestShapeValue);
        skinnedMeshRenderer.SetBlendShapeWeight(6, womanChestShapeValue);

        womanHipCmMin = womanHipCmMin + (womanHipCmMin * willAddMeasurementRatio / 100);
        womanHipCmMax = womanHipCmMax + (womanHipCmMax * willAddMeasurementRatio / 100);
        womanHipShapeValue = ((womanHipInput - womanHipCmMin) * 100) / (womanHipCmMax - womanHipCmMin);
        skinnedMeshRenderer.SetBlendShapeWeight(2, womanHipShapeValue);
        skinnedMeshRenderer.SetBlendShapeWeight(7, womanHipShapeValue); //underwear ile o b�lge ayn� oranda artt�r�l�yor

        skinnedMeshRenderer.SetBlendShapeWeight(3, (womanHipShapeValue + womanWaistShapeValue) / 2);
        skinnedMeshRenderer.SetBlendShapeWeight(4, (womanHipShapeValue + womanWaistShapeValue) / 2);
        skinnedMeshRenderer.SetBlendShapeWeight(5, (womanHipShapeValue + womanWaistShapeValue) / 2);
    }
}