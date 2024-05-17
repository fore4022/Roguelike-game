using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;

public class Inventory_UI : UI_Scene
{
    public SwipeMenu_UI swipeMenu;

    private Scrollbar verticalScrollBar;
    private RectTransform content;

    enum Buttons
    {
        //
    }
    enum Images
    {
        Panel,
        Panel1,
        Panel2,
        ScrollView
    }
    enum ScrollBars
    {
        ScrollBarVertical
    }
    private void Start() 
    {
        init();

        if (SceneManager.GetActiveScene().name == "Main")
        {
            Transform pos = GameObject.Find($"{this.GetType().Name.Replace("_UI", "")}" + "Page").transform;

            this.gameObject.transform.SetParent(pos);
            RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();

            rectTransform.sizeDelta = pos.GetComponentInParent<RectTransform>().rect.size;
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;

            swipeMenu = FindObjectOfType<SwipeMenu_UI>().GetComponent<SwipeMenu_UI>();
        }
    }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<Scrollbar>(typeof(ScrollBars));

        GameObject scrollView = get<Image>((int)Images.ScrollView).gameObject;

        verticalScrollBar = get<Scrollbar>((int)ScrollBars.ScrollBarVertical);
        content = FindChild<RectTransform>(this.gameObject, "Content", true);

        AddUIEvent(scrollView, (PointerEventData data) =>
        {
            if (SceneManager.GetActiveScene().name == "Main")
            {
#if UNITY_EDITOR
                swipeMenu.enterPoint = Input.mousePosition;
#endif

#if UNITY_ANDROID
                swipeMenu.enterPoint = Input.GetTouch(0).position; 
#endif
            }
            else
            {
                //
            }
        }, Define.UIEvent.BeginDrag);
        AddUIEvent(scrollView, (PointerEventData data) =>
        {
            if (SceneManager.GetActiveScene().name == "Main") //
            {

#if UNITY_EDITOR
                swipeMenu.direction = (Vector2)Input.mousePosition - swipeMenu.enterPoint;// direction...
#endif

#if UNITY_ANDROID
                swipeMenu.direction = Input.GetTouch(0).position - swipeMenu.enterPoint;
#endif
                if (Mathf.Abs(swipeMenu.direction.y) > Mathf.Abs(swipeMenu.direction.x)) { Scroll(); }
            }
            else
            {
                //
            }
        }, Define.UIEvent.Drag);
        AddUIEvent(scrollView, (PointerEventData data) =>
        {
            if (SceneManager.GetActiveScene().name == "Main")
            {
                if (Mathf.Abs(swipeMenu.direction.x) > Mathf.Abs(swipeMenu.direction.y)) { swipeMenu.StartCoroutine(swipeMenu.relocation()); }
            }
            else 
            {
                //if(Mathf.Abs())
            }
        }, Define.UIEvent.EndDrag);

        for(int h = 0; h < Mathf.Max(Managers.Game.c / 4 + (Managers.Game.c % 4 > 0 ? 1 : 0), 5); h++)//
        {
            for(int w = 0; w < 4; w++)
            {
                GameObject go = Managers.Resource.instantiate("UI/Slot", content.transform);
                RectTransform rectTransform = go.GetComponent<RectTransform>();

                rectTransform.localScale = new Vector2(1, 1);
                rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTransform.anchoredPosition = new Vector2(0.5f, 0.5f);
                rectTransform.localPosition = new Vector2(-360 + 240 * w, 480 - 240 * h);

                AddUIEvent(go, (PointerEventData data) =>
                {
                    
                }, Define.UIEvent.Click);
                AddUIEvent(go, (PointerEventData data) =>
                {

                }, Define.UIEvent.BeginDrag);
                AddUIEvent(go, (PointerEventData data) =>
                {

                }, Define.UIEvent.Drag);
                AddUIEvent(go, (PointerEventData data) =>
                {

                }, Define.UIEvent.EndDrag);
            }
        }
    }
    private void Scroll()
    {
        
    }
}