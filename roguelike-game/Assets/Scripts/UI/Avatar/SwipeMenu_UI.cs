using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor.Experimental.GraphView;

public class SwipeMenu_UI : UI_Scene
{
    public float relocationDelay = 0.15f;

    private Vector2 enterPoint;
    private Vector2 direction;

    private float relocationValue;
    private int origin;

    private RectTransform panel;
    enum Buttons
    {
        StoreButton,
        MainButton,
        BelongingsButton
    }
    enum Images
    {
        DragAndDropHandler,
        Panel
    }
    enum TMPro
    {
        StoreTxt,
        MainTxt,
        BelongingsTxt
    }
    private void OnEnable() { origin = 0; }
    private void Start()
    {
        init();

        relocationValue = this.gameObject.GetComponent<RectTransform>().sizeDelta.x;

        Managers.UI.showSceneUI<Store_UI>("Store");
        Managers.UI.showSceneUI<Main_UI>("Main");
        Managers.UI.showSceneUI<Belongings_UI>("Belongings");
    }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));

        GameObject storeButton = get<Button>((int)Buttons.StoreButton).gameObject;
        GameObject mainButton = get<Button>((int)Buttons.MainButton).gameObject;
        GameObject belongingsButton = get<Button>((int)Buttons.BelongingsButton).gameObject;
        GameObject dragAndDropHandler = get<Image>((int)Images.DragAndDropHandler).gameObject;

        panel = get<Image>((int)Images.Panel).gameObject.GetComponent<RectTransform>();
        panel.pivot = panel.position = new Vector2(0, 0);

        AddUIEvent(storeButton, (PointerEventData data) =>
        {
            origin = -1;
            StartCoroutine(relocation());
        }, Define.UIEvent.Click);

        AddUIEvent(mainButton, (PointerEventData data) =>
        {
            origin = 0;
            StartCoroutine(relocation());
        }, Define.UIEvent.Click);

        AddUIEvent(belongingsButton, (PointerEventData data) =>
        {
            origin = 1;
            StartCoroutine(relocation());
        }, Define.UIEvent.Click);

        AddUIEvent(dragAndDropHandler, (PointerEventData data) =>
        {
#if UNITY_EDITOR
            enterPoint = Input.mousePosition;
#endif
#if UNITY_ANDROID
            enterPoint = Input.GetTouch(0).position;
#endif
        }, Define.UIEvent.BeginDrag);
        AddUIEvent(dragAndDropHandler, (PointerEventData data) =>
        {
#if UNITY_EDITOR
            direction = (Vector2)Input.mousePosition - enterPoint;
#endif
#if UNITY_ANDROID
            direction = Input.GetTouch(0).position - enterPoint;
#endif
            if(direction.x != 0) 
            {
                switch(origin)
                {
                    case -1:
                        if(direction.x > 0) { panel.position = new Vector3(direction.x, 0, 0); }
                        break;
                    case 1:
                        if(direction.x < 0) { panel.position = new Vector3(direction.x, 0, 0); }
                        break;
                    default:
                        panel.position = new Vector3(direction.x, 0, 0);
                        break;
                }
            }
        }, Define.UIEvent.Drag);
        AddUIEvent(dragAndDropHandler, (PointerEventData data) =>
        {
            if(direction.x > relocationValue / 3) { StartCoroutine(relocation()); }
        }, Define.UIEvent.EndDrag);
    }
    private IEnumerator relocation()
    {
        float timer = 0;

        if(direction.x > 0) { origin++; }
        else { origin--; }

        while (timer == relocationDelay / (Mathf.Abs(direction.x) / relocationValue))
        {
            panel.position += new Vector3((relocationValue - Mathf.Abs(direction.x)) * (relocationDelay / (Mathf.Abs(direction.x) / relocationValue)), 0, 0) * origin;
            timer += Time.deltaTime;
            yield return null;
        }
    }
}