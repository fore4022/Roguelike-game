using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using System.Reflection;

public class Inventory_UI : UI_Scene
{
    public Sprite[] sprites;

    public SwipeMenu_UI swipeMenu;

    private List<(Item item, Sprite sprite, int count, bool isEquipped)> slotDatas = new List<(Item item, Sprite sprite, int count, bool isEquipped)>();
    private List<Slot_UI> slotList = new List<Slot_UI>();
    private List<Item> itemList;

    private RectTransform content;
    private ScrollRect inventoryScrollView;
    private Transform pos;
    private Image itemImage;

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

        if (itemList == null) { itemList = Managers.Resource.LoadAll<Item>("Data/Item/").ToList<Item>(); }

        init();

        can.renderMode = RenderMode.ScreenSpaceOverlay;
        can.overrideSorting = false;
        can.sortingOrder = FindObjectOfType<SwipeMenu_UI>().GetComponent<Canvas>().sortingOrder + 1;

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

        Managers.Data.edit -= updateSlot;
        Managers.Data.edit += updateSlot;
    }
    protected override void init()
    {
        base.init();
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
            if (pos != null) { evtHandle.OnBeginDragHandler.Invoke(data); }//
#if UNITY_EDITOR
            enterPoint = Input.mousePosition;
#endif

#if UNITY_ANDROID
            enterPoint = Input.GetTouch(0).position;
#endif
        }, Define.UIEvent.BeginDrag);
        AddUIEvent(scrollView, (PointerEventData data) =>
        {
            if (pos != null) { evtHandle.OnDragHandler.Invoke(data); }//
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
            if (pos != null) { evtHandle.OnEndDragHandler.Invoke(data); }//
        }, Define.UIEvent.EndDrag);

        AddUIEvent(itemImage.gameObject, (PointerEventData data) =>
        {
            if(equippedItemIndex == -1) { return; }

            Managers.UI.showPopupUI<ItemInformation_UI>("ItemInformation");
            Managers.UI.PopupStack.Peek().gameObject.GetComponent<ItemInformation_UI>().setData(slotDatas[equippedItemIndex].item, slotDatas[equippedItemIndex].sprite, slotDatas[equippedItemIndex].count, slotDatas[equippedItemIndex].isEquipped);
        }, Define.UIEvent.Click);

        setSlot();
    }
    private void Scroll() { inventoryScrollView.verticalScrollbar.value = Mathf.Clamp(inventoryScrollView.verticalScrollbar.value + direction.y / (content.rect.height + 245 * (height / 5)), 0, 1); }
    private void setSlot()
    {
        UI_EventHandler evtHandle = FindParent<UI_EventHandler>(content.gameObject);

        height = Managers.Data.inventoryData.Count / 4 + (Managers.Data.inventoryData.Count % 4 > 0 ? 1 : 0);
        equippedItemIndex = -1;
        index = -1;

        for (int i = 0; i < (height < 4 ? 4 : height) * 4; i++)
        {
            GameObject go = Managers.Resource.instantiate("UI/Slot", content.transform);

            Slot_UI slot = go.AddComponent<Slot_UI>();

            if (SceneManager.GetActiveScene().name == "InGame")
            {
                if (!(slot.item.GetType() == System.Type.GetType("Equipment"))) { slotList.Add(slot); }
                else { continue; }
            }
            else if (SceneManager.GetActiveScene().name == "Main") { slotList.Add(slot); }

            index = Mathf.Min(++index, Managers.Data.inventoryData.Count - 1);

            List<Item> item = checkList(index);

            if (i >= Managers.Data.inventoryData.Count) { slot.setSlot(null, null, -1); }
            else { slotList[i].setSlot(item[0], Array.Find(sprites, sprite => sprite.name == Managers.Data.inventoryData[index].itemName), Managers.Data.inventoryData[index].count, Managers.Data.inventoryData[index].equipped == 0 ? false : true); }

            slotDatas.Add((slot.item, slot.sprite, slot.count, slot.isEquipped));

            int _index = index;

            AddUIEvent(go, (PointerEventData data) =>
            {
                Slot_UI slot = go.GetComponent<Slot_UI>();

                selectedItemIndex = _index;

                if (slot.item == null) { return; }

                Managers.UI.showPopupUI<ItemInformation_UI>("ItemInformation");
                Managers.UI.PopupStack.Peek().gameObject.GetComponent<ItemInformation_UI>().setData(slotDatas[_index].item, slotDatas[_index].sprite, slotDatas[_index].count, slotDatas[_index].isEquipped);
            }, Define.UIEvent.Click);
            AddUIEvent(go, (PointerEventData data) => { evtHandle.OnBeginDragHandler.Invoke(data); }, Define.UIEvent.BeginDrag);
            AddUIEvent(go, (PointerEventData data) => { evtHandle.OnDragHandler.Invoke(data); }, Define.UIEvent.Drag);
            AddUIEvent(go, (PointerEventData data) => { evtHandle.OnEndDragHandler.Invoke(data); }, Define.UIEvent.EndDrag);
        }

        content.offsetMin = new Vector2(content.offsetMin.x, content.offsetMin.y + 245 * Mathf.Abs(height - 4));

        if (equippedItemIndex == -1) { itemImage.gameObject.SetActive(false); }
        else { itemImage.gameObject.SetActive(true); }
    }
    private void updateSlot() 
    {
        index = -1;

        for (int i = 0; i < slotList.Count; i++)
        {
            index = Mathf.Min(++index, Managers.Data.inventoryData.Count - 1);

            List<Item> item = checkList(index);

            if (i >= Managers.Data.inventoryData.Count()) 
            {
                slotList[i].setSlot(null, null, -1);

                continue;
            }
            else
            {
                if (updateEquippedSlot(Managers.Data.inventoryData[index].equipped))
                {
                    equippedItemIndex = index;
                    itemImage.sprite = Array.Find(sprites, sprite => sprite.name == Managers.Data.inventoryData[index].itemName);
                }

                slotList[i].setSlot(item[0], Array.Find(sprites, sprite => sprite.name == Managers.Data.inventoryData[index].itemName), Managers.Data.inventoryData[index].count, Managers.Data.inventoryData[index].equipped == 0 ? false : true);
            }

            slotDatas[i] = (slotList[i].item, slotList[i].sprite, slotList[i].count, slotList[i].isEquipped);

            if (Managers.UI.PopupStack.Peek().gameObject.GetComponent<ItemInformation_UI>() != null)
            {
                if (selectedItemIndex == i) { Managers.UI.PopupStack.Peek().gameObject.GetComponent<ItemInformation_UI>().updateData(slotDatas[index].item, slotDatas[index].sprite, slotDatas[index].count, slotDatas[index].isEquipped); }
            }
            else { selectedItemIndex = -1; }
        }

        if(equippedItemIndex == -1) 
        {
            itemImage.gameObject.SetActive(false);

            if (Managers.UI.PopupStack.Peek().GetComponent<ItemInformation_UI>()) { Managers.UI.closePopupUI(); }
        }
        else { itemImage.gameObject.SetActive(true); }
    }
    private bool updateEquippedSlot(int isEquipped) { return isEquipped == 1 ? true : false; }
    private List<Item> checkList(int index)
    {
        List<Item> item = itemList.Select(item => item.itemName == Managers.Data.inventoryData[index].itemName ? item : null).ToList(); ;
        item.RemoveAll(item => item == null);
        return item;
    }
    private void OnDisable() { Managers.Data.edit -= updateSlot; }
}