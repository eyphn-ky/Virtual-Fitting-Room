using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuggestSize : MonoBehaviour
{
    class Size
    {
        public float Waist { get; set; }
        public float Hip { get; set; }
        public float Chest { get; set; }
        public float Height { get; set; }
    }
    class SizeWithName : Size
    {
        public string SizeName { get; set; }
    }
    
    public GameObject suggestUpperSizeTextBox;
    public GameObject suggestLowerSizeTextBox;

    Size user = new Size();
    SizeWithName XSSize = new SizeWithName();
    SizeWithName SSize = new SizeWithName();
    SizeWithName MSize = new SizeWithName();
    SizeWithName LSize = new SizeWithName();
    SizeWithName XLSize = new SizeWithName();
    float MainHeight;
    List<SizeWithName> Sizes = new List<SizeWithName>();
    void SizeSetter()
    {

        MainHeight = 165.5f;
        XSSize.SizeName = "XS";
        XSSize.Hip =75;
        XSSize.Waist =68;
        XSSize.Chest =76;
        XSSize.Height = MainHeight;

        SSize.SizeName = "S";
        SSize.Hip =85;
        SSize.Waist =72;
        SSize.Chest =85;
        SSize.Height = MainHeight;

        MSize.SizeName = "M";
        MSize.Hip =95;
        MSize.Waist =76;
        MSize.Chest =94;
        MSize.Height = MainHeight;

        LSize.SizeName = "L";
        LSize.Hip =105;
        LSize.Waist =80;
        LSize.Chest =103;
        LSize.Height = MainHeight;

        XLSize.SizeName = "XL";
        XLSize.Hip =115;
        XLSize.Waist =84;
        XLSize.Chest =111;
        XLSize.Height = MainHeight;

        Sizes.Add(XSSize);
        Sizes.Add(SSize);
        Sizes.Add(MSize);
        Sizes.Add(LSize);
        Sizes.Add(XLSize);
    }
    void Start()
    {
        SizeSetter();
        user.Chest = 68;
        user.Waist =76;
        user.Hip = 115;
        user.Height = 170f;

        foreach(var size in Sizes)
        {
            if(size.Waist >= user.Waist && size.Chest >= user.Chest )
            {
                suggestUpperSizeTextBox.GetComponent<Text>().text = size.SizeName;
                break;
            }
            else
            {
                suggestUpperSizeTextBox.GetComponent<Text>().text = "Uygun ürün bulamadýk";
            }
        }
        foreach (var size in Sizes)
        {
            if (size.Hip >= user.Hip)
            {
                suggestLowerSizeTextBox.GetComponent<Text>().text = size.SizeName;
                break;
            }
            else
            {
                suggestLowerSizeTextBox.GetComponent<Text>().text = "Uygun ürün bulamadýk";
            }
        }

    }
    void Update()
    {

    }
}
