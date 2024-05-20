using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Merchandise_UI : UI_Scene
{
    private Image merchandise;
    private TextMeshProUGUI productName;
    private TextMeshProUGUI price;
    enum Images
    {
        Purchase,
        Merchandise
    }
    enum TMPro
    {
        ProductName,
        Price
    }
    private void Start() { init(); }
    protected override void init()
    {
        base.init();
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));

        productName = get<TextMeshProUGUI>((int)TMPro.ProductName);
        price = get<TextMeshProUGUI>((int)TMPro.Price);

        AddUIEvent()
    }
}
