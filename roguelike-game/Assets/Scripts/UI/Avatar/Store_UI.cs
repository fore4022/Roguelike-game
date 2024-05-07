using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Store_UI : UI_Scene
{
    private GameObject merchandise1;
    private GameObject merchandise2;
    private GameObject merchandise3;
    private GameObject merchandise4;
    private GameObject merchandise5;
    private GameObject merchandise6;

    private TextMeshProUGUI productName1;
    private TextMeshProUGUI productName2;
    private TextMeshProUGUI productName3;
    private TextMeshProUGUI productName4;
    private TextMeshProUGUI productName5;
    private TextMeshProUGUI productName6;

    private TextMeshProUGUI rating1;
    private TextMeshProUGUI rating2;
    private TextMeshProUGUI rating3;
    private TextMeshProUGUI rating4;
    private TextMeshProUGUI rating5;
    private TextMeshProUGUI rating6;

    private TextMeshProUGUI price1;
    private TextMeshProUGUI price2;
    private TextMeshProUGUI price3;
    private TextMeshProUGUI price4;
    private TextMeshProUGUI price5;
    private TextMeshProUGUI price6;
    enum Buttons
    {
        Refresh
    }
    enum Images
    {
        Merchandise1,
        Merchandise2,
        Merchandise3,
        Merchandise4,
        Merchandise5,
        Merchandise6
    }
    enum TMPro
    {
        ProductName1,
        ProductName2,
        ProductName3,
        ProductName4,
        ProductName5,
        ProductName6,
        Rating1,
        Rating2,
        Rating3,
        Rating4,
        Rating5,
        Rating6,
        price1,
        price2,
        price3,
        price4,
        price5,
        price6
    }
    private void Start()
    {
        init();
        Transform pos = GameObject.Find($"{this.GetType().Name.Replace("_UI", "")}" + "Page").transform;

        this.gameObject.transform.SetParent(pos);
        RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();

        rectTransform.sizeDelta = pos.GetComponentInParent<RectTransform>().rect.size;
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;
    }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));

        GameObject refresh = get<Button>((int)Buttons.Refresh).gameObject;
    }
}
