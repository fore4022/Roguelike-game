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
    private void Start() { Init(); }
    protected override void Init()
    {
        base.Init();
        bind<Image>(typeof(Images));

        GameObject pause = get<Image>((int)Images.Pause).gameObject;

        AddUIEvent(pause, (PointerEventData data) => 
        {
            Managers.UI.ShowPopupUI<Menu_UI>("Menu");
        }, Define.UIEvent.Click); 
    }
}
