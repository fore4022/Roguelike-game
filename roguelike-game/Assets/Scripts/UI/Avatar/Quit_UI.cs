using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Quit_UI : UI_Popup
{
    enum Images
    {
        Quit
    }
    private void Start() { init(); }
    protected override void init()
    {
        base.init();
        bind<Image>(typeof(Images));
        GameObject quit = get<Image>((int)Images.Quit).gameObject;
        quit.AddComponent<Button>();
        AddUIEvent(quit, (PointerEventData data) => { Application.Quit(); }, Define.UIEvent.Click);
    }
}
