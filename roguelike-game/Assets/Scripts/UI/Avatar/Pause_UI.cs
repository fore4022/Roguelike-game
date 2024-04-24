using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Pause_UI : UI_Scene
{
    enum Images
    {
        Pause
    }
    private void Start() { init(); }
    protected override void init()
    {
        base.init();
        bind<Image>(typeof(Images));
        GameObject pause = get<Image>((int)Images.Pause).gameObject;
        AddUIEvent(pause, (PointerEventData data) => 
        {
            Managers.UI.showPopupUI<Menu_UI>("Menu");
        }, Define.UIEvent.Click); 
        AddUIEvent(pause, (PointerEventData data) =>
        {

        }, Define.UIEvent.Enter);
    }
}
