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
        Panel2
    }
    enum ScrollViews
    {
        InventorySlider
    }
    enum TMPro
    {
        Gold
    }
    private void Start() 
    {
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

        init();
    }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<ScrollRect>(typeof(ScrollViews));
        bind<TextMeshProUGUI>(typeof(TMPro));

        GameObject inventorySlider = get<ScrollRect>((int)ScrollViews.InventorySlider).gameObject;

        AddUIEvent(inventorySlider, (PointerEventData data) =>
        {
            if (SceneManager.GetActiveScene().name == "Main") { swipeMenu.enterPoint = Input.mousePosition; }
            else
            {

            }
        }, Define.UIEvent.BeginDrag);
        AddUIEvent(inventorySlider, (PointerEventData data) =>
        {
            if (SceneManager.GetActiveScene().name == "Main") { swipeMenu.direction = Input.GetTouch(0).position - swipeMenu.enterPoint; }
            else
            {

            }
        }, Define.UIEvent.Drag);
        AddUIEvent(inventorySlider, (PointerEventData data) =>
        {
            if (SceneManager.GetActiveScene().name == "Main") { swipeMenu.StartCoroutine(swipeMenu.relocation()); }
            else
            {

            }
        }, Define.UIEvent.EndDrag);
    }
}
