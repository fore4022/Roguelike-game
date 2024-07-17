using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;
[Obsolete]
public class StageSelection_UI : UI_Scene
{
    private GameObject _panel;

    private bool _visibleStageInformation = false;

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
        StageImage,
        Panel
    }
    enum TMPro
    {
        StageName,
        StageInformation
    }
    private void OnEnable() { GameObject.Find("Synthesis").SetActive(false); }
    private void Start()
    {
        Init();

        Transform pos = GameObject.Find("MainPage").transform;

        this.gameObject.transform.SetParent(pos);
        RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();

        rectTransform.sizeDelta = pos.GetComponentInParent<RectTransform>().rect.size;
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;
    }
    protected override void Init()
    {
        base.Init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));

        GameObject exit = get<Button>((int)Buttons.Exit).gameObject;
        GameObject select = get<Button>((int)Buttons.Select).gameObject;
        GameObject leftArrow = get<Button>((int)Buttons.LeftArrow).gameObject;
        GameObject rightArrow = get<Button>((int)Buttons.RightArrow).gameObject;
        GameObject backgroundImage = get<Image>((int)Images.BackgroundImage).gameObject;
        GameObject stageImage = get<Image>((int)Images.StageImage).gameObject;

        _panel = get<Image>((int)Images.Panel).gameObject;

        AddUIEvent(exit, (PointerEventData data) => { Managers.UI.CloseSceneUI(); }, Define.UIEvent.Click);

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

        AddUIEvent(stageImage, (PointerEventData data) =>
        {
            if(_visibleStageInformation)
            {
                _visibleStageInformation = !_visibleStageInformation;
                //
            }
            else
            {
                _visibleStageInformation = !_visibleStageInformation;
                Set();
                //
            }
            _panel.SetActive(_visibleStageInformation);

        }, Define.UIEvent.Click);

        Set();
    }
    private void Set()
    {
        //
    }
}