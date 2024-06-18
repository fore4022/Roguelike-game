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
    private TextMeshProUGUI text1;
    private TextMeshProUGUI text2;

    private string sceneName;

    enum Buttons
    {
        Exit,
        Button1,
        Button2
    }
    enum Images
    {
        Background,
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
        StatText3,
        Text1,
        Text2
    }
    public void set(object _item, Sprite _sprite, int _count, bool _isEquipped = false)
    {
        sceneName = SceneManager.GetActiveScene().name;
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

        Image background = get<Image>((int)Images.Background);
        Image itemImage = get<Image>((int)Images.ItemImage);

        TextMeshProUGUI itemName = get<TextMeshProUGUI>((int)TMPro.ItemName);
        TextMeshProUGUI itemInformation = get<TextMeshProUGUI>((int)TMPro.ItemInformation);

        statTexts.Add(get<TextMeshProUGUI>((int)TMPro.StatText1)); 
        statTexts.Add(get<TextMeshProUGUI>((int)TMPro.StatText2));
        statTexts.Add(get<TextMeshProUGUI>((int)TMPro.StatText3));

        statImages.Add(get<Image>((int)Images.StatImage1));
        statImages.Add(get<Image>((int)Images.StatImage2));
        statImages.Add(get<Image>((int)Images.StatImage3));

        button1 = get<Button>((int)Buttons.Button1).gameObject;
        button2 = get<Button>((int)Buttons.Button2).gameObject;

        text1 = get<TextMeshProUGUI>((int)TMPro.Text1);
        text2 = get<TextMeshProUGUI>((int)TMPro.Text2);

        AddUIEvent(exit, (PointerEventData data) => { closePopup(); }, Define.UIEvent.Click);

        AddUIEvent(background.gameObject, (PointerEventData data) => { closePopup(); }, Define.UIEvent.Click);

        sprites = Managers.Resource.LoadAll<Sprite>("sprites/Icon/Stat");

        Item _item = null;
        
        if (item.ConvertTo(item.GetType()).GetType() == System.Type.GetType("Equipment"))
        {
            _item = set_Equipment();

            AddUIEvent(button1, (PointerEventData data) =>
            {
                isEquipped = !isEquipped;

                Managers.Data.inventory_edit(_item.itemName, 0, isEquipped ? 1 : 0);

                set_Equipment();
            }, Define.UIEvent.Click);

            button2.SetActive(false);
        }
        else if (item.ConvertTo(item.GetType()).GetType() == System.Type.GetType("Expendables"))
        {
            _item = set_Expendables();
        }
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

                if(statType != "Penetrate") { statTexts[index].text = $"{equipment[i]}"; }
                else { statTexts[index].text = ""; }

                statImages[index].sprite = Array.Find(sprites, sprite => sprite.name == statType);
                index++;
            }
        }
        
        while(index < 3)
        {
            statTexts[index].gameObject.transform.parent.gameObject.SetActive(false);
            index++;
        }

        if (isEquipped)
        {
            text1.text = "해제";
            text2.text = "판매";
        }
        else
        {
            text1.text = "장착";
            text2.text = "판매";
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

                statTexts[index].text = $"{expendables}";
                statImages[index].sprite = Array.Find(sprites, sprite => sprite.name == statType);
                index++;
            }
        }

        while (index < 3)
        {
            statTexts[index].gameObject.transform.parent.gameObject.SetActive(false);
            index++;
        }

        return expendables;
    }
}