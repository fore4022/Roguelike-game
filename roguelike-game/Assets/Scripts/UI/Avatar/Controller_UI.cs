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
        ControllerUnit,
        ControllerBar
    }
    private void Awake() { init(); }
    private void OnEnable() { controllerUnit.transform.position = Input.GetTouch(0).position; }
    private void Update() 
    {
        if (Managers.Game.player.Hp < 0) { controllerBar.transform.position = Input.GetTouch(0).position; }
        else { Managers.UI.closePopupUI(); }
    }
    protected override void init()
    {
        base.init();
        bind<Image>(typeof(Images));

        controllerUnit = get<Image>((int)Images.ControllerUnit).gameObject;
        controllerBar = get<Image>((int)Images.ControllerBar).gameObject;
    }
}
