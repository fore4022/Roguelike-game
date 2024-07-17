using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Controller_UI : UI_Popup
{
    private GameObject _controllerUnit;
    private GameObject _controllerBar;
    enum Images
    {
        ControllPanel,
        ControllerUnit,
        ControllerBar
    }
    private void Start()
    {
        Init();
        _controllerUnit.SetActive(false);
    }
    private void Update() 
    {
        if (Managers.Game.player.Hp > 0)
        {
            if(Input.touchCount > 0)
            {
                if (_controllerUnit.activeSelf) { _controllerBar.transform.position = Managers.Game.player.enterPoint + Vector2.ClampMagnitude(Input.GetTouch(0).position - Managers.Game.player.enterPoint, 100); }
            }
        }
        else { ClosePopup(); }
    }
    protected override void Init()
    {
        base.Init();
        bind<Image>(typeof(Images));

        GameObject controllPanel = get<Image>((int)Images.ControllPanel).gameObject;

        _controllerUnit = get<Image>((int)Images.ControllerUnit).gameObject;
        _controllerBar = get<Image>((int)Images.ControllerBar).gameObject;

        AddUIEvent(controllPanel, (PointerEventData data) =>
        {
            _controllerUnit.SetActive(true);
            _controllerUnit.transform.position = Input.GetTouch(0).position;
        }, Define.UIEvent.Down);
        AddUIEvent(controllPanel, (PointerEventData data) =>
        {
            _controllerUnit.SetActive(false);
        }, Define.UIEvent.Up);
    }
}