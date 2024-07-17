using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Menu_UI : UI_Popup
{
    private GameObject _reStart;
    private GameObject _belongings;
    private GameObject _quit;
    private GameObject _setting;
    private Image _panel;
    enum Images
    {
        Panel,
        ReStart,
        Belongings,
        Quit,
        Setting
    }
    private void Start() { Init(); }
    private void OnEnable() { Time.timeScale = 0f; }
    protected override void Init()
    {
        base.Init();
        bind<Image>(typeof(Images));
        _reStart = get<Image>((int)Images.ReStart).gameObject;
        _belongings = get<Image>((int)Images.Belongings).gameObject;
        _quit = get<Image>((int)Images.Quit).gameObject;
        _setting = get<Image>((int)Images.Setting).gameObject;
        _panel = get<Image>((int)Images.Panel);

        _panel.color = new Color(255f, 255f, 255f, 0.3f);

        AddUIEvent(_reStart, (PointerEventData data) => { StartCoroutine(ReStart()); }, Define.UIEvent.Click); ;

        AddUIEvent(_belongings, (PointerEventData data) => { Managers.UI.ShowSceneUI<Inventory_UI>("Inventory"); }, Define.UIEvent.Click);

        AddUIEvent(_quit, (PointerEventData data) => 
        {
            ClosePopup();
            Managers.UI.ShowSceneUI<Inventory_UI>("Inventory");
        }, Define.UIEvent.Click);

        AddUIEvent(_setting, (PointerEventData data) =>
        {
            //
        }, Define.UIEvent.Click);
    }
    private IEnumerator ReStart()
    {
        _reStart.SetActive(false);
        _belongings.SetActive(false);
        _quit.SetActive(false);
        _setting.SetActive(false);

        int i = 0;

        while(true)
        {
            float value = 1f - (i / 255f);

            _panel.color = new Color(value, value, value, 0.3f);
            i += 2;

            if (i >= 160) { break; }

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
        }

        Time.timeScale = 1f;
        ClosePopup();
    }
}
