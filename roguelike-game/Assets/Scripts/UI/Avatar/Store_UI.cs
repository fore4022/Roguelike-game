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
        //
    }
    enum TMPro
    {
        Store
    }
    private void Start()
    {
        init();
        Transform pos = GameObject.Find($"{this.GetType().Name.Replace("_UI", "")}" + "Page").transform;

        this.gameObject.transform.SetParent(pos);
        RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();

        rectTransform.sizeDelta = pos.GetComponentInParent<RectTransform>().rect.size;
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;
    }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));

        GameObject refresh = get<Button>((int)Buttons.Refresh).gameObject;

        AddUIEvent(refresh, (PointerEventData data) =>
        {
            //
        },Define.UIEvent.Click);

    }
}
