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
        Exit,
        Button1,
        Button2
    }
    enum Images
    {
        ItemImage
    }
    enum TMPro
    {
        ItemName,
        ItemInformation,
        Stat1,
        Stat2,
        Stat3
    }
    public void set(Item _item, Sprite _sprite, int _count, bool _isEquipped)
    {
        item = _item;
        sprite = _sprite;
        count = _count;
        isEquipped = _isEquipped;

        init();
    }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));

        GameObject exit = get<Button>((int)Buttons.Exit).gameObject;
        GameObject button1 = get<Button>((int)Buttons.Button1).gameObject;
        GameObject button2 = get<Button>((int)Buttons.Button2).gameObject;

        Image itemImage = get<Image>((int)Images.ItemImage);

        TextMeshProUGUI itemName = get<TextMeshProUGUI>((int)TMPro.ItemName);
        TextMeshProUGUI itemInformation = get<TextMeshProUGUI>((int)TMPro.ItemInformation);
        TextMeshProUGUI stat1 = get<TextMeshProUGUI>((int)TMPro.Stat1);
        TextMeshProUGUI stat2 = get<TextMeshProUGUI>((int)TMPro.Stat2);
        TextMeshProUGUI stat3 = get<TextMeshProUGUI>((int)TMPro.Stat3);

        itemImage.sprite = sprite;
        itemName.text = $"{item.itemName}";

        AddUIEvent(exit, (PointerEventData data) => { closePopup(); }, Define.UIEvent.Click);

        if (item.GetType() == System.Type.GetType("Equipment")) { set_Equipment(); }
        else if (item.GetType() == System.Type.GetType("Expendables")) { set_Expendables(); }
        else { closePopup(); }
    }
    private void set_Equipment()
    {
        
    }
    private void set_Expendables()
    {

    }
}
//mounting
//clear
//sell
//Expendables
//equipment
//use
//Unequipped