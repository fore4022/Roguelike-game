using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
public class Inventory_UI : UI_Scene
{    
    public SwipeMenu_UI swipeMenu;
    enum Buttons
    {

    }
    enum Images
    {
        Panel,
        Panel1,
        Panel2,
        ScrollView
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

        GameObject scrollView = get<Image>((int)Images.ScrollView).gameObject;

        AddUIEvent(scrollView, (PointerEventData data) =>
        {
            if (SceneManager.GetActiveScene().name == "Main")
            {
#if UNITY_EDITOR
                swipeMenu.enterPoint = Input.mousePosition;
#endif

#if UNITY_ANDROID
                swipeMenu.enterPoint = Input.mousePosition; 
#endif
            }
            else
            {

            }
        }, Define.UIEvent.BeginDrag);
        AddUIEvent(scrollView, (PointerEventData data) =>
        {
            if (SceneManager.GetActiveScene().name == "Main") 
            {
#if UNITY_EDITOR
                swipeMenu.direction = (Vector2)Input.mousePosition - swipeMenu.enterPoint;
#endif

#if UNITY_ANDROID
                swipeMenu.direction = Input.GetTouch(0).position - swipeMenu.enterPoint;
#endif
            }
            else
            {

            }
        }, Define.UIEvent.Drag);
        AddUIEvent(scrollView, (PointerEventData data) =>
        {
            if (SceneManager.GetActiveScene().name == "Main")
            {
                if (swipeMenu.direction.x > swipeMenu.direction.y) { swipeMenu.StartCoroutine(swipeMenu.relocation()); }
                else if (swipeMenu.direction.y > swipeMenu.direction.x) { StartCoroutine(Scroll()); }
            }
            else 
            {
                //
            }
        }, Define.UIEvent.EndDrag);

        //
    }
    private IEnumerator Scroll()
    {
        while(true)
        {
            yield return null;
        }
    }
}