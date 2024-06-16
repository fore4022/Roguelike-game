using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using static UnityEditor.Progress;
using System.Linq.Expressions;
using System;

public class ItemInformation_UI : UI_Popup
{
    public object item;
    public Sprite sprite;

    public int count;
    public bool isEquipped;

    private List<TextMeshProUGUI> statTexts = new List<TextMeshProUGUI>();
    private List<Image> statImages = new List<Image>();
    private Sprite[] sprites;

    private GameObject button1;
    private GameObject button2;
    enum Buttons
    {
        Exit,
        Button1,
        Button2
    }
    enum Images
    {
        ItemImage,
        StatImage1,
        StatImage2,
        StatImage3
    }
    enum TMPro
    {
        ItemName,
        ItemInformation,
        StatText1,
        StatText2,
        StatText3
    }
    public void set(object _item, Sprite _sprite, int _count, bool _isEquipped)
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

        statTexts.Add(get<TextMeshProUGUI>((int)TMPro.StatText1)); 
        statTexts.Add(get<TextMeshProUGUI>((int)TMPro.StatText2));
        statTexts.Add(get<TextMeshProUGUI>((int)TMPro.StatText3));

        statImages.Add(get<Image>((int)Images.StatImage1));
        statImages.Add(get<Image>((int)Images.StatImage2));
        statImages.Add(get<Image>((int)Images.StatImage3));

        AddUIEvent(exit, (PointerEventData data) => { closePopup(); }, Define.UIEvent.Click);

        sprites = Managers.Resource.LoadAll<Sprite>("sprites/Icon/Stat");

        Item _item = null;
        
        if (item.ConvertTo(item.GetType()).GetType() == System.Type.GetType("Equipment")) { _item = set_Equipment(); }
        else if (item.ConvertTo(item.GetType()).GetType() == System.Type.GetType("Expendables")) { _item = set_Expendables(); }
        else { closePopup(); }

        itemImage.sprite = sprite;
        itemName.text = $"{_item.itemName}";
        itemInformation.text = $"{_item.explanation}";
    }
    private Item set_Equipment()
    {
        Equipment equipment = (Equipment)item.ConvertTo(System.Type.GetType("Equipment"));
        int index = 0;
        string statType = "";

        for (int i = 0; i < 7; i++)
        {
            if (equipment[i] != null)
            {
                switch(i)
                {
                    case 0:
                        statType = "Range";
                        break;
                    case 1:
                        statType = "Attack Delay";
                        break;
                    case 2:
                        statType = "Attack Damage";
                        break;
                    case 3:
                        statType = "CollDown";
                        break;
                    case 4:
                        statType = "Speed";
                        break;
                    case 5:
                        statType = "Health";
                        break;
                    case 6:
                        statType = "Penetrate";
                        break;
                }

                statTexts[index].text = $"{statType} {equipment[i]}";
                statImages[index].sprite = Array.Find(sprites, sprite => sprite.name == statType);
                index++;
            }
        }
        
        while(index < 3)
        {
            statTexts[index].gameObject.transform.parent.gameObject.SetActive(false);
            index++;
        }

        return equipment;
    }
    private Item set_Expendables()
    {
        Expendables expendables = (Expendables)item.ConvertTo(System.Type.GetType("Expendables"));
        int index = 0;
        string statType = "";

        for (int i = 0; i < 9; i++)
        {
            if (expendables[i] != 0)
            {
                switch(i)
                {
                    case 0:
                        statType = "Attack Damage";
                        break;
                    case 1:
                        statType = "CollDown";
                        break;
                    case 2:
                        statType = "Health";
                        break;
                    case 3:
                        statType = "Speed";
                        break;
                    case 4:
                        statType = "Eyesight";
                        break;
                    case 5:
                        statType = "Gold Magnification";
                        break;
                    case 6:
                        statType = "Level";
                        break;
                    case 7:
                        statType = "Health Regeneration Per Second";
                        break;
                    case 8:
                        statType = "Exp Magnification";
                        break;
                }

                statTexts[index].text = $"{statType} {expendables}";
                index++;
            }
        }

        return expendables;
    }
}
//itemName.text = $"{item.itemName}";