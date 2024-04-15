using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Quit_UI : UI_Popup
{
    enum Buttons
    {
        Quit
    }
    private void Start()
    {
        init();
    }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        GameObject quit = get<Button>((int)Buttons.Quit).gameObject;
        AddUIEvent(quit, (PointerEventData data) => { Application.Quit(); }, Define.UIEvent.Click);
    }
}
