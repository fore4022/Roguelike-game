using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class SelectedItem_UI : UI_Popup
{
    enum Buttons
    {

    }
    enum Images
    {

    }
    enum TMPro
    {

    }
    private void Start() { Init(); }
    protected override void Init()
    {
        base.Init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));
    }
}
