using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Encyclopedia_UI : UI_Popup
{
    enum Images
    {

    }
    enum TMPro
    {

    }
    private void Start() { init(); }
    protected override void init()
    {
        base.init();
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));
    }
}
/*
page1, page2, page3
*/