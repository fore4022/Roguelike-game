using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Slot_UI : Util
{
    public Item item;
    public int count;

    private Image itemImage;
    private TextMeshProUGUI itemCount;

    public void setSlot(Item _item, Sprite sprite, int _count, bool isEquipped = false)
    {
        if (itemImage == null || itemCount == null)
        {
            itemImage = FindChild<Image>(this.gameObject, "slotImage", true);
            itemCount = FindChild<TextMeshProUGUI>(this.gameObject, "slotCount", true);
        }
        
        if(sprite == null) { itemImage.gameObject.SetActive(false); }
        else { itemImage.gameObject.SetActive(true); }

        item = _item;
        count = (int)_count;

        itemImage.sprite = sprite;
        if (isEquipped) { itemCount.text = "Equipped"; }
        else 
        {
            if (_count == -1) { itemCount.text = ""; }
            else { itemCount.text = $"X  {_count}"; }
        }
    }
}