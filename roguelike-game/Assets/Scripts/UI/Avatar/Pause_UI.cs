using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Pause_UI : UI_Scene
{
    enum Buttons
    {
        Pause
    }
    private void Start() { init(); }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        GameObject pause = get<Button>((int)Buttons.Pause).gameObject;
        AddUIEvent(pause, (PointerEventData data) => 
        {
            Managers.UI.closePopupUI();
            Managers.UI.showPopupUI<Menu_UI>("Menu");
        }, Define.UIEvent.Click); 
    }
}
