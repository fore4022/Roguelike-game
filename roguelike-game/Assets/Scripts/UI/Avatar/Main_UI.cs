using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
public class Main_UI : UI_Scene
{
    private TextMeshProUGUI timer;
    private TextMeshProUGUI stageName;
    enum Buttons
    {
        Start
    }
    enum Images
    {
        StagePanel
    }
    enum TMPro
    {
        StageName
    }
    private void Start() 
    {
        Transform pos = GameObject.Find($"{this.GetType().Name.Replace("_UI", "")}" + "Page").transform;
        Canvas can = this.gameObject.GetComponent<Canvas>();

        this.gameObject.transform.SetParent(pos);
        RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();

        rectTransform.sizeDelta = pos.GetComponentInParent<RectTransform>().rect.size;
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;

        Init();

        can.renderMode = RenderMode.ScreenSpaceOverlay;
        can.overrideSorting = false;
        can.sortingOrder = FindObjectOfType<SwipeMenu_UI>().GetComponent<Canvas>().sortingOrder + 1;
    }
    private void Update() { /*timer.text = $"{}";*/ }
    protected override void Init()
    {
        base.Init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));

        GameObject start = get<Button>((int)Buttons.Start).gameObject;
        GameObject stagePanel = get<Image>((int)Images.StagePanel).gameObject;

        stageName = get<TextMeshProUGUI>((int)TMPro.StageName);

        AddUIEvent(start, (PointerEventData data) =>
        {
            SceneManager.LoadScene(1);
        }, Define.UIEvent.Click);

        UI_EventHandler evtHandle = FindParent<UI_EventHandler>(this.gameObject);

        AddUIEvent(stagePanel, (PointerEventData data) => { Managers.UI.showSceneUI<StageSelection_UI>("StageSelection"); }, Define.UIEvent.Click);
        AddUIEvent(stagePanel, (PointerEventData data) =>
        {
#if UNITY_ANDROID
            evtHandle.OnBeginDragHandler.Invoke(data);
#endif
        }, Define.UIEvent.BeginDrag);
        AddUIEvent(stagePanel, (PointerEventData data) =>
        {
#if UNITY_ANDROID
            evtHandle.OnDragHandler.Invoke(data);
#endif
        }, Define.UIEvent.Drag);
        AddUIEvent(stagePanel, (PointerEventData data) =>
        {
#if UNITY_ANDROID
            evtHandle.OnEndDragHandler.Invoke(data);
#endif
        }, Define.UIEvent.EndDrag);
    }
}
