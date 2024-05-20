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

    private bool visibleStageInformation = false;

    enum Buttons
    {
        Exit,
        Select,
        LeftArrow,
        RightArrow
    }
    enum Images
    {
        BackgroundImage,
        StageImage
    }
    enum TMPro
    {
        StageName,
        StageInformation
    }
    private void Start() { init(); }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));

        GameObject exit = get<Button>((int)Buttons.Exit).gameObject;
        GameObject select = get<Button>((int)Buttons.Select).gameObject;
        GameObject leftArrow = get<Button>((int)Buttons.LeftArrow).gameObject;
        GameObject rightArrow = get<Button>((int)Buttons.RightArrow).gameObject;
        GameObject backgroundImage = get<Image>((int)Images.BackgroundImage).gameObject;
        GameObject stageImage = get<Image>((int)Images.StageImage).gameObject;

        AddUIEvent(exit, (PointerEventData data) => { Managers.UI.closeSceneUI(); }, Define.UIEvent.Click);

        AddUIEvent(select, (PointerEventData data) =>
        {
            //
        }, Define.UIEvent.Click);

        AddUIEvent(leftArrow, (PointerEventData data) =>
        {
            //
        }, Define.UIEvent.Click);

        AddUIEvent(rightArrow, (PointerEventData data) =>
        {
            //
        }, Define.UIEvent.Click);

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
            if(visibleStageInformation)
            {
                //
            }
            else
            {
                //
            }
        }, Define.UIEvent.Click);
    }
}