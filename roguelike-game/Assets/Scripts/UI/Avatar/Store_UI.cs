using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Store_UI : UI_Scene
{
    public List<Item> itemDatas = new List<Item>();

    enum Buttons
    {
        Refresh
    }
    enum Images
    {
        StorePanel
    }
    enum TMPro
    {
        Store
    }
    private void Start()
    {
        Canvas can = this.gameObject.GetComponent<Canvas>();

        Init();

        can.renderMode = RenderMode.ScreenSpaceOverlay;
        can.overrideSorting = false;
        can.sortingOrder = FindObjectOfType<SwipeMenu_UI>().GetComponent<Canvas>().sortingOrder + 1;

        Transform pos = GameObject.Find($"{this.GetType().Name.Replace("_UI", "")}" + "Page").transform;

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

        GameObject refresh = get<Button>((int)Buttons.Refresh).gameObject;
        GameObject storePanel = get<Image>((int)Images.StorePanel).gameObject;

        AddUIEvent(refresh, (PointerEventData data) =>
        {
            //
        },Define.UIEvent.Click);

        for(int h = 0; h < 2; h++)
        {
            for(int w = 0; w < 3; w++)
            {
                GameObject go = Managers.Resource.instantiate("UI/Merchandise", storePanel.transform);
                RectTransform rectTrnasform = go.GetComponent<RectTransform>();

                rectTrnasform.localScale = new Vector2(1, 1);
                rectTrnasform.anchorMax = new Vector2(0.5f, 0.5f);
                rectTrnasform.anchorMin = new Vector2(0.5f, 0.5f);
                rectTrnasform.anchoredPosition = new Vector2(0.5f, 0.5f);
                rectTrnasform.localPosition = new Vector2(-350 + 350 * w, 250 - 450 * h);

                AddUIEvent(go, (PointerEventData data) =>
                {
                    //showPopupUI
                }, Define.UIEvent.Click);
            }
        }
    }
}
