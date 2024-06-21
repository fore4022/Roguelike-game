using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
public class Inventory_UI : UI_Scene
{
    public Sprite[] sprites;

    public SwipeMenu_UI swipeMenu;

    private List<(Item item, Sprite sprite, int count, bool isEquipped)> slotDataList = new List<(Item item, Sprite sprite, int count, bool isEquipped)>();
    private List<Slot_UI> slotList = new List<Slot_UI>();
    private List<Item> itemList;

    private RectTransform _content;
    private ScrollRect _inventoryScrollView;
    private Transform _pos;
    private Image _itemImage;
    private UI_EventHandler _evtHandle;

    private Vector2 _enterPoint;
    private Vector2 _direction;

    private int _height;
    private int _index;
    private int _equippedItemIndex = -1;
    enum Images
    {
        Panel,
        Panel1,
        Panel2,
        ScrollView,
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

        if (SceneManager.GetActiveScene().name == "Main") { _pos = GameObject.Find($"{this.GetType().Name.Replace("_UI", "")}" + "Page").transform; }
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

        if (_pos != null)
        {
            this.gameObject.transform.SetParent(_pos);
            RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();

            rectTransform.sizeDelta = _pos.GetComponentInParent<RectTransform>().rect.size;
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

        _itemImage = get<Image>((int)Images.ItemImage);
        _inventoryScrollView = get<ScrollRect>((int)ScrollRects.InventoryScrollView);

        _content = _inventoryScrollView.content.gameObject.GetComponent<RectTransform>();

        _inventoryScrollView.movementType = ScrollRect.MovementType.Clamped;

        UI_EventHandler evtHandle = null;

        if (_pos != null) { evtHandle = FindParent<UI_EventHandler>(_pos.gameObject); }

        AddUIEvent(scrollView, (PointerEventData data) =>
        {
            if (_pos != null) { evtHandle.OnBeginDragHandler.Invoke(data); }
#if UNITY_EDITOR
            _enterPoint = Input.mousePosition;
#endif

#if UNITY_ANDROID
            _enterPoint = Input.GetTouch(0).position;
#endif
        }, Define.UIEvent.BeginDrag);
        AddUIEvent(scrollView, (PointerEventData data) =>
        {
            if (_pos != null) { evtHandle.OnDragHandler.Invoke(data); }
#if UNITY_EDITOR
            _direction = (Vector2)Input.mousePosition - _enterPoint;
#endif

#if UNITY_ANDROID
            _direction = Input.GetTouch(0).position - _enterPoint;
#endif
            if (Mathf.Abs(_direction.y) / Mathf.Abs(_direction.x) > 0.5f) { Scroll(); }
        }, Define.UIEvent.Drag);
        AddUIEvent(scrollView, (PointerEventData data) =>
        {
            if (_pos != null) { evtHandle.OnEndDragHandler.Invoke(data); }
        }, Define.UIEvent.EndDrag);

        AddUIEvent(_itemImage.gameObject, (PointerEventData data) =>
        {
            if(_equippedItemIndex == -1) { return; }

            Managers.UI.ShowPopupUI<ItemInformation_UI>("ItemInformation");
            Managers.UI.PopupStack.Peek().gameObject.GetComponent<ItemInformation_UI>().SetData(slotDataList[_equippedItemIndex].item, slotDataList[_equippedItemIndex].sprite, slotDataList[_equippedItemIndex].count, slotDataList[_equippedItemIndex].isEquipped);
        }, Define.UIEvent.Click);

        SetSlot();
    }
    private void Scroll() { _inventoryScrollView.verticalScrollbar.value = Mathf.Clamp(_inventoryScrollView.verticalScrollbar.value + _direction.y / (_content.rect.height + 245 * (_height / 5)), 0, 1); }
    private void SetSlot()
    {
        Slot_UI slot;
        GameObject go;
        Item item;

        _evtHandle = FindParent<UI_EventHandler>(_content.gameObject);

        _height = Managers.Data.inventoryData.Count / 4 + (Managers.Data.inventoryData.Count % 4 > 0 ? 1 : 0);
        _equippedItemIndex = -1;
        _index = -1;

        for (int i = 0; i < (_height < 4 ? 4 : _height) * 4; i++)
        {
            go = Managers.Resource.Instantiate("UI/Slot", _content.transform);

            slot = go.AddComponent<Slot_UI>();

            slotList.Add(slot);

            _index = Mathf.Min(++_index, Managers.Data.inventoryData.Count);

            if(_index == Managers.Data.inventoryData.Count || i >= Managers.Data.inventoryData.Count)
            {
                slot.SetSlot(null, null, -1, _evtHandle);

                continue; 
            }

            item = CheckList();

            if (Managers.Data.inventoryData[i].equipped == 0 ? false : true)
            {
                _equippedItemIndex = _index;
                _itemImage.sprite = Array.Find(sprites, sprite => sprite.name == Managers.Data.inventoryData[_index].itemName);
            }

            slotList[i].SetSlot(item, Array.Find(sprites, sprite => sprite.name == Managers.Data.inventoryData[_index].itemName), Managers.Data.inventoryData[_index].count, _evtHandle, Managers.Data.inventoryData[_index].equipped == 0 ? false : true);

            slotDataList.Add((slot._item, slot._sprite, slot._count, slot._isEquipped));
        }

        _content.offsetMin = new Vector2(_content.offsetMin.x, _content.offsetMin.y + 245 * Mathf.Abs(_height - 4));

        if (_equippedItemIndex == -1) { _itemImage.gameObject.SetActive(false); }
        else { _itemImage.gameObject.SetActive(true); }
    }
    private void UpdateSlot() 
    {
        Item item;

        _index = -1;

        for (int i = 0; i < slotList.Count; i++)
        {
            _index = Mathf.Min(++_index, Managers.Data.inventoryData.Count);

            if (_index == Managers.Data.inventoryData.Count || i >= Managers.Data.inventoryData.Count)
            {
                slotList[i].SetSlot(null, null, -1, _evtHandle);

                continue;
            }

            item = CheckList();

            if (Managers.Data.inventoryData[_index].equipped == 1 ? true : false)
            {
                _equippedItemIndex = _index;
                _itemImage.sprite = Array.Find(sprites, sprite => sprite.name == Managers.Data.inventoryData[_index].itemName);
            }

            slotList[i].SetSlot(item, Array.Find(sprites, sprite => sprite.name == Managers.Data.inventoryData[_index].itemName), Managers.Data.inventoryData[_index].count, _evtHandle, Managers.Data.inventoryData[_index].equipped == 0 ? false : true);
        }

        if (_equippedItemIndex == -1) { _itemImage.gameObject.SetActive(false); }
        else { _itemImage.gameObject.SetActive(true); }
    }
    private Item CheckList()
    {
        List<Item> item = itemList.Select(item => item.itemName == Managers.Data.inventoryData[_index].itemName ? item : null).ToList(); ;
        item.RemoveAll(item => item == null);

        if(SceneManager.GetActiveScene().name == "InGame") 
        {
            while(item.Count == 0)
            {
                _index++;

                item = itemList.Select(item => item.itemName == Managers.Data.inventoryData[_index].itemName ? item : null).ToList(); ;
                item.RemoveAll(item => item == null);
            }
        }

        return item[0];
    }
    private void OnDisable() { Managers.Data.edit -= UpdateSlot; }
}