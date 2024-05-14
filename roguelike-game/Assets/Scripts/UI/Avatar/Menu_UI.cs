using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Menu_UI : UI_Popup
{
    private GameObject reStart;
    private GameObject belongings;
    private GameObject quit;
    private GameObject setting;
    private Image panel;
    enum Images
    {
        Panel,
        ReStart,
        Belongings,
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
        reStart = get<Image>((int)Images.ReStart).gameObject;
        belongings = get<Image>((int)Images.Belongings).gameObject;
        quit = get<Image>((int)Images.Quit).gameObject;
        setting = get<Image>((int)Images.Setting).gameObject;
        panel = get<Image>((int)Images.Panel);

        panel.color = new Color(255f, 255f, 255f, 0.3f);

        AddUIEvent(reStart, (PointerEventData data) => { StartCoroutine(rStart()); }, Define.UIEvent.Click); ;
        AddUIEvent(belongings, (PointerEventData data) => { Managers.UI.showSceneUI<Inventory_UI>("Inventory"); }, Define.UIEvent.Click);
        AddUIEvent(quit, (PointerEventData data) => 
        {
            Managers.UI.closePopupUI();
            Managers.UI.showSceneUI<Inventory_UI>("Inventory");
        }, Define.UIEvent.Click);
        AddUIEvent(setting, (PointerEventData data) =>
        {
            //
        }, Define.UIEvent.Click);
    }
    private IEnumerator rStart()
    {
        reStart.SetActive(false);
        belongings.SetActive(false);
        quit.SetActive(false);
        setting.SetActive(false);

        int i = 0;
        while(true)
        {
            float value = 1f - (i / 255f);
            panel.color = new Color(value, value, value, 0.3f);
            i += 2;
            if (i >= 160) { break; }
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
        }

        Managers.Game.stopWatch.Start();
        Time.timeScale = 1f;
        Managers.UI.closePopupUI();
    }
}
