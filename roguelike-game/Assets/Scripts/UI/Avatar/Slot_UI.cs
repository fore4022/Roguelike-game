using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;
public class Slot_UI : Util
{
    public Item _item;
    public Sprite _sprite;

    public int _count;
    public bool _isEquipped;

    private Image _itemImage;
    private TextMeshProUGUI _itemCount;

    public void setSlot(Item item, Sprite sprite, int count, UI_EventHandler eventHandle, bool isEquipped = false)
    {
        if (_itemImage == null || _itemCount == null)
        {
            _itemImage = FindChild<Image>(this.gameObject, "slotImage", true);
            _itemCount = FindChild<TextMeshProUGUI>(this.gameObject, "slotCount", true);
        }
        
        if(sprite == null) { _itemImage.gameObject.SetActive(false); }
        else { _itemImage.gameObject.SetActive(true); }

        _item = item;
        _count = (int)count;
        _sprite = sprite;
        _isEquipped = isEquipped;
        _itemImage.sprite = sprite;

        if (count == -1) { _itemCount.text = ""; }
        else { _itemCount.text = $"X  {count}"; }

        AddUIEvent(this.gameObject, (PointerEventData data) =>
        {
            if (this._item == null) { return; }
            Debug.Log("asdf");
            Managers.UI.showPopupUI<ItemInformation_UI>("ItemInformation");
            Managers.UI.PopupStack.Peek().gameObject.GetComponent<ItemInformation_UI>().setData(_item, _sprite, _count, _isEquipped);
        }, Define.UIEvent.Click);
        AddUIEvent(this.gameObject, (PointerEventData data) => { eventHandle.OnBeginDragHandler.Invoke(data); }, Define.UIEvent.BeginDrag);
        AddUIEvent(this.gameObject, (PointerEventData data) => { eventHandle.OnDragHandler.Invoke(data); }, Define.UIEvent.Drag);
        AddUIEvent(this.gameObject, (PointerEventData data) => { eventHandle.OnEndDragHandler.Invoke(data); }, Define.UIEvent.EndDrag);
    }
}