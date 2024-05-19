using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class StageSelection_UI : UI_Scene
{
    private Vector2 enterPoint;
    private Vector2 direction;

    enum Buttons
    {
        Exit
    }
    enum Images
    {
        BackgroundImage,
        StageImage
    }
    enum TMPro
    {
        StageName,
        Information
    }
    enum ScrollRects
    {
        //
    }
    private void Start() { init(); }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));
        bind<ScrollRect>(typeof(ScrollRects));

        GameObject exit = get<Button>((int)Buttons.Exit).gameObject;
        GameObject backgroundImage = get<Image>((int)Images.BackgroundImage).gameObject;
        GameObject stageImage = get<Image>((int)Images.StageImage).gameObject;

        AddUIEvent(exit, (PointerEventData data) => { Managers.UI.closeSceneUI(); }, Define.UIEvent.Click);

        AddUIEvent(backgroundImage, (PointerEventData data) =>
        {
            //
        }, Define.UIEvent.Click);
        AddUIEvent(backgroundImage, (PointerEventData data) =>
        {
#if UNITY_EDITOR
            enterPoint = Input.mousePosition;
#endif

#if UNITY_ANDROID
            enterPoint = Input.GetTouch(0).position;
#endif
        }, Define.UIEvent.BeginDrag);
        AddUIEvent(backgroundImage, (PointerEventData data) =>
        {
#if UNITY_EDITOR
            direction = (Vector2)Input.mousePosition - enterPoint;
#endif

#if UNITY_ANDROID
            direction = Input.GetTouch(0).position - enterPoint;
#endif
        }, Define.UIEvent.Drag);
        AddUIEvent(backgroundImage, (PointerEventData data) =>
        {
            //
        }, Define.UIEvent.EndDrag);

        AddUIEvent(stageImage, (PointerEventData data) =>
        {
            //
        }, Define.UIEvent.Click);
    }
}