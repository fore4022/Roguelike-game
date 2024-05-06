using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Controller_UI : UI_Popup
{
    private GameObject controllerUnit;
    private GameObject controllerBar;
    enum Images
    {
        ControllPanel,
        ControllerUnit,
        ControllerBar
    }
    private void Start()
    {
        init();
        controllerUnit.SetActive(false);
    }
    private void Update() 
    {
        if (Managers.Game.player.Hp > 0)
        {
            if(Input.touchCount > 0)
            {
                if (controllerUnit.activeSelf) { controllerBar.transform.position = Managers.Game.player.enterPoint + Vector2.ClampMagnitude(Input.GetTouch(0).position - Managers.Game.player.enterPoint, 100); }
            }
        }
        else { Managers.UI.closePopupUI(); }
    }
    protected override void init()
    {
        base.init();
        bind<Image>(typeof(Images));

        GameObject controllPanel = get<Image>((int)Images.ControllPanel).gameObject;
        controllerUnit = get<Image>((int)Images.ControllerUnit).gameObject;
        controllerBar = get<Image>((int)Images.ControllerBar).gameObject;

        AddUIEvent(controllPanel, (PointerEventData data) =>
        {
            controllerUnit.SetActive(true);
            controllerUnit.transform.position = Input.GetTouch(0).position;
        }, Define.UIEvent.Down);
        AddUIEvent(controllPanel, (PointerEventData data) =>
        {
            controllerUnit.SetActive(false);
        }, Define.UIEvent.Up);
    }
}