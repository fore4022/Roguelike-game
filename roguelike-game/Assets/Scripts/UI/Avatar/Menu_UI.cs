using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Menu_UI : UI_Popup
{
    enum Images
    {
        ReStart,
        Stat,
        Quit,
        Setting
    }
    private void Start() { init(); }
    private void OnEnable() 
    {
        Time.timeScale = 0f;
        Managers.Game.stopWatch.Stop();
    }
    protected override void init()
    {
        base.init();
        bind<Image>(typeof(Images));
        GameObject reStart = get<Image>((int)Images.ReStart).gameObject;
        GameObject stat = get<Image>((int)Images.Stat).gameObject;
        GameObject quit = get<Image>((int)Images.Quit).gameObject;
        GameObject setting = get<Image>((int)Images.Setting).gameObject;
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
        AddUIEvent(setting, (PointerEventData data) =>
        {
            //
        }, Define.UIEvent.Click);
    }
}
