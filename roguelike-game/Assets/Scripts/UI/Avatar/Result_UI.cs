using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Result_UI : UI_Popup
{
    enum TMPro
    {

    }
    private void Start() { init(); }
    protected override void init()
    {
        base.init();
        bind<TextMeshProUGUI>(typeof(TMPro));
    }
}
