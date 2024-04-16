using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Menu_UI : UI_Popup
{
    enum Buttons
    {
        ReStart,
        Stat,
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
        GameObject reStart = get<Button>((int)Buttons.ReStart).gameObject;
        GameObject stat = get<Button>((int)Buttons.Stat).gameObject;
        GameObject quit = get<Button>((int)Buttons.Quit).gameObject;
        AddUIEvent(reStart, (PointerEventData data) =>
        {
            Time.timeScale = 1f;
            Managers.UI.closePopupUI();
        }, Define.UIEvent.Click); ;
        AddUIEvent(stat, (PointerEventData data) => { Managers.UI.showPopupUI<Stat_UI>("Stat"); }, Define.UIEvent.Click);
        AddUIEvent(quit, (PointerEventData data) => 
        {
            Managers.UI.closePopupUI();
            //Managers.UI.showPopupUI<>();
        }, Define.UIEvent.Click);
    }
}
