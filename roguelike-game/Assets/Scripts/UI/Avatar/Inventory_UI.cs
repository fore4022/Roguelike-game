using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
public class Inventory_UI : UI_Scene
{
    public Sprite[] sprites;

    public SwipeMenu_UI swipeMenu;

    public string itemName = "";

    private List<(Item item, Sprite sprite, int count, bool isEquipped)> slotDataList = new List<(Item item, Sprite sprite, int count, bool isEquipped)>();
    private List<Slot_UI> slotList = new List<Slot_UI>();
    private List<Item> itemList;

    private RectTransform content;
    private ScrollRect inventoryScrollView;
    private Transform pos;
    private Image itemImage;
    private UI_EventHandler evtHandle;

    private Vector2 enterPoint;
    private Vector2 direction;

    private int height;
    private int index;
    private int equippedItemIndex = -1;
    private int selectedItemIndex = -1;

    enum Images
    {
        Panel,
        Panel1,
        Panel2,
        ScrollView,
        Background,
        ItemImage
    }
    enum ScrollRects
    {
        InventoryScrollView
    }

    private void Start()
    {
        Canvas can = this.gameObject.GetComponent<Canvas>();

        sprites = Managers.Resource.LoadAll<Sprite>("sprites/Icon/item");

        if (SceneManager.GetActiveScene().name == "Main") { pos = GameObject.Find($"{this.GetType().Name.Replace("_UI", "")}" + "Page").transform; }
        else { }//another Scene

        if (itemList == null) 
        {
            itemList = Managers.Resource.LoadAll<Item>("Data/Item/").ToList<Item>();

            if (SceneManager.GetActiveScene().name == "InGame") { itemList.RemoveAll(item => item.GetType() == System.Type.GetType("Equipment")); }
        }

        Init();

        can.renderMode = RenderMode.ScreenSpaceOverlay;
        can.overrideSorting = false;
        
        if(SceneManager.GetActiveScene().name == "Main") { can.sortingOrder = FindObjectOfType<SwipeMenu_UI>().GetComponent<Canvas>().sortingOrder + 1; }

        if (pos != null)
        {
            this.gameObject.transform.SetParent(pos);
            RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();

            rectTransform.sizeDelta = pos.GetComponentInParent<RectTransform>().rect.size;
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;

            swipeMenu = FindObjectOfType<SwipeMenu_UI>().GetComponent<SwipeMenu_UI>();
        }

        Managers.Data.edit -= UpdateSlot;
        Managers.Data.edit += UpdateSlot;
    }
    protected override void Init()
    {
        base.Init();
        bind<Image>(typeof(Images));
        bind<ScrollRect>(typeof(ScrollRects));

        GameObject scrollView = get<Image>((int)Images.ScrollView).gameObject;
        GameObject background = get<Image>((int)Images.Background).gameObject;

        itemImage = get<Image>((int)Images.ItemImage);
        inventoryScrollView = get<ScrollRect>((int)ScrollRects.InventoryScrollView);

        content = inventoryScrollView.content.gameObject.GetComponent<RectTransform>();

        inventoryScrollView.movementType = ScrollRect.MovementType.Clamped;

        AddUIEvent(background, (PointerEventData data) =>
        {

        }, Define.UIEvent.Click);

        UI_EventHandler evtHandle = null;

        if (pos != null) { evtHandle = FindParent<UI_EventHandler>(pos.gameObject); }

        AddUIEvent(scrollView, (PointerEventData data) =>
        {
            if (pos != null) { evtHandle.OnBeginDragHandler.Invoke(data); }
#if UNITY_EDITOR
            enterPoint = Input.mousePosition;
#endif

#if UNITY_ANDROID
            enterPoint = Input.GetTouch(0).position;
#endif
        }, Define.UIEvent.BeginDrag);
        AddUIEvent(scrollView, (PointerEventData data) =>
        {
            if (pos != null) { evtHandle.OnDragHandler.Invoke(data); }
#if UNITY_EDITOR
            direction = (Vector2)Input.mousePosition - enterPoint;
#endif

#if UNITY_ANDROID
            direction = Input.GetTouch(0).position - enterPoint;
#endif
            if (Mathf.Abs(direction.y) / Mathf.Abs(direction.x) > 0.5f) { Scroll(); }
        }, Define.UIEvent.Drag);
        AddUIEvent(scrollView, (PointerEventData data) =>
        {
            if (pos != null) { evtHandle.OnEndDragHandler.Invoke(data); }
        }, Define.UIEvent.EndDrag);

        AddUIEvent(itemImage.gameObject, (PointerEventData data) =>
        {
            if(equippedItemIndex == -1) { return; }

            Managers.UI.showPopupUI<ItemInformation_UI>("ItemInformation");
            Managers.UI.PopupStack.Peek().gameObject.GetComponent<ItemInformation_UI>().setData(slotDataList[equippedItemIndex].item, slotDataList[equippedItemIndex].sprite, slotDataList[equippedItemIndex].count, slotDataList[equippedItemIndex].isEquipped);
        }, Define.UIEvent.Click);

        SetSlot();
    }
    private void Scroll() { inventoryScrollView.verticalScrollbar.value = Mathf.Clamp(inventoryScrollView.verticalScrollbar.value + direction.y / (content.rect.height + 245 * (height / 5)), 0, 1); }
    private void SetSlot()
    {
        evtHandle = FindParent<UI_EventHandler>(content.gameObject);

        height = Managers.Data.inventoryData.Count / 4 + (Managers.Data.inventoryData.Count % 4 > 0 ? 1 : 0);
        equippedItemIndex = -1;
        index = -1;
        itemName = "";

        for (int i = 0; i < (height < 4 ? 4 : height) * 4; i++)
        {
            GameObject go = Managers.Resource.instantiate("UI/Slot", content.transform);

            Slot_UI slot = go.AddComponent<Slot_UI>();

            slotList.Add(slot);

            index = Mathf.Min(++index, Managers.Data.inventoryData.Count);

            if(index == Managers.Data.inventoryData.Count || i >= Managers.Data.inventoryData.Count)
            {
                slot.setSlot(null, null, -1, evtHandle);

                continue; 
            }

            List<Item> item = CheckList();

            if (Managers.Data.inventoryData[i].equipped == 0 ? false : true)
            {
                equippedItemIndex = index;
                itemImage.sprite = Array.Find(sprites, sprite => sprite.name == Managers.Data.inventoryData[index].itemName);
            }

            slotList[i].setSlot(item[0], Array.Find(sprites, sprite => sprite.name == Managers.Data.inventoryData[index].itemName), Managers.Data.inventoryData[index].count, evtHandle, Managers.Data.inventoryData[index].equipped == 0 ? false : true);

            slotDataList.Add((slot._item, slot._sprite, slot._count, slot._isEquipped));

            if (slot._isEquipped) { itemName = $"{slot._item.itemName}"; }
        }

        content.offsetMin = new Vector2(content.offsetMin.x, content.offsetMin.y + 245 * Mathf.Abs(height - 4));

        if (equippedItemIndex == -1) { itemImage.gameObject.SetActive(false); }
        else { itemImage.gameObject.SetActive(true); }
    }
    private void UpdateSlot() 
    {
        index = -1;
        itemName = "";

        for (int i = 0; i < slotList.Count; i++)
        {
            index = Mathf.Min(++index, Managers.Data.inventoryData.Count);

            if (index == Managers.Data.inventoryData.Count || i >= Managers.Data.inventoryData.Count)
            {
                slotList[i].setSlot(null, null, -1, evtHandle);

                continue;
            }

            List<Item> item = CheckList();

            if (Managers.Data.inventoryData[index].equipped == 1 ? true : false)
            {
                equippedItemIndex = index;
                itemImage.sprite = Array.Find(sprites, sprite => sprite.name == Managers.Data.inventoryData[index].itemName);
            }

            slotList[i].setSlot(item[0], Array.Find(sprites, sprite => sprite.name == Managers.Data.inventoryData[index].itemName), Managers.Data.inventoryData[index].count, evtHandle, Managers.Data.inventoryData[index].equipped == 0 ? false : true);
        }

        if (itemName == "") { itemImage.gameObject.SetActive(false); }
        else { itemImage.gameObject.SetActive(true); }
    }
    private List<Item> CheckList()
    {
        List<Item> item = itemList.Select(item => item.itemName == Managers.Data.inventoryData[index].itemName ? item : null).ToList(); ;
        item.RemoveAll(item => item == null);

        if(SceneManager.GetActiveScene().name == "InGame") 
        {
            while(item.Count == 0)
            {
                index++;

                item = itemList.Select(item => item.itemName == Managers.Data.inventoryData[index].itemName ? item : null).ToList(); ;
                item.RemoveAll(item => item == null);
            }
        }

        return item;
    }
    private void OnDisable() { Managers.Data.edit -= UpdateSlot; }
}