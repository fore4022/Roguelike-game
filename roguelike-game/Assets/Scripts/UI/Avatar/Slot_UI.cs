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

    public void setSlot(Item _item, Sprite sprite, int _count)//
    {
        if (itemImage == null || itemCount == null)
        {
            itemImage = FindChild<Image>(this.gameObject, "slotImage", true);
            itemCount = FindChild<TextMeshProUGUI>(this.gameObject, "slotCount", true);
        }

        item = _item;
        count = _count;

        itemImage.sprite = sprite;
        itemCount.text = $"{_count}";
    }
}