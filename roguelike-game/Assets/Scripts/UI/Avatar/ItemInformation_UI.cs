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

public class ItemInformation_UI : UI_Popup
{
    public object item;
    public Sprite sprite;

    public int count;
    public bool isEquipped;

    private List<TextMeshProUGUI> stats = new List<TextMeshProUGUI>();
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

        stats.Add(get<TextMeshProUGUI>((int)TMPro.Stat1)); 
        stats.Add(get<TextMeshProUGUI>((int)TMPro.Stat2));
        stats.Add(get<TextMeshProUGUI>((int)TMPro.Stat3));

        AddUIEvent(exit, (PointerEventData data) => { closePopup(); }, Define.UIEvent.Click);

        //var _item;
        
        if (item.ConvertTo(item.GetType()).GetType() == System.Type.GetType("Equipment")) { set_Equipment(); }
        else if (item.ConvertTo(item.GetType()).GetType() == System.Type.GetType("Expendables")) { set_Expendables(); }
        else { closePopup(); }

        itemImage.sprite = sprite;
    }
    private void set_Equipment()
    {
        Equipment equipment = (Equipment)item.ConvertTo(System.Type.GetType("Equipment"));
        int index = 0;

        for (int i = 0; i < 7; i++)
        {
            if (equipment[i] != null)
            {
                string stat = "";

                switch(i)
                {
                    case 0:
                        stat = "Range";
                        break;
                    case 1:
                        stat = "Attack Delay";
                        break;
                    case 2:
                        stat = "Attack Damage";
                        break;
                    case 3:
                        stat = "CollDown";
                        break;
                    case 4:
                        stat = "Speed";
                        break;
                    case 5:
                        stat = "health";
                        break;
                    case 6:
                        stat = "penetrate";
                        break;
                }

                stats[index].text = $"{stat} {equipment[i]}";
                index++;
            }
        }
    }
    private void set_Expendables()
    {
        Expendables expendables = (Expendables)item.ConvertTo(System.Type.GetType("Expendables"));
        int index = 0;

        for(int i = 0; i < 9; i++)
        {

        }
    }
}
//itemName.text = $"{item.itemName}";