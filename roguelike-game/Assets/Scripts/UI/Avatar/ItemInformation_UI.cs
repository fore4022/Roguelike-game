using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class ItemInformation_UI : UI_Popup
{
    public Item item;
    public Sprite sprite;

    public int count;
    public bool isEquipped;
    enum Buttons
    {

    }
    enum Images
    {

    }
    enum TMPro
    {
        itemName,
        itemInformation
    }
    private void Start() { init(); }
    public void set(Item _item, Sprite _sprite, int _count, bool _isEquipped)
    {
        item = _item;
        sprite = _sprite;
        count = _count;
        isEquipped = _isEquipped;
    }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));


    }
}
