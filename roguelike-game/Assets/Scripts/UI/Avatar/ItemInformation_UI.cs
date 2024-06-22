using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System;
using static UnityEditor.Progress;

public class ItemInformation_UI : UI_Popup
{
    private List<TextMeshProUGUI> statTexts = new List<TextMeshProUGUI>();
    private List<Image> statImages = new List<Image>();
    private Sprite[] sprites;

    private Item _item;

    private GameObject _button1;
    private GameObject _button2;
    private TextMeshProUGUI _text1;
    private TextMeshProUGUI _text2;
    private Sprite _sprite;

    private int _count;
    private bool _isEquipped;

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
    public void SetData(Item item, Sprite sprite, int count, bool isEquipped = false)
    {
        _item = item;
        _sprite = sprite;
        _count = count;
        _isEquipped = isEquipped;

        Init();
    }
    public void UpdateData(Item item, Sprite sprite, int count, bool isEquipped = false)
    {
        _item = item;
        _sprite = sprite;
        _count = count;
        _isEquipped = isEquipped;

        var itemType = _item.ConvertTo(_item.GetType());

        if (itemType.GetType() == Type.GetType("Equipment")) { SetEquipment((Equipment)itemType); }
        else { SetExpendables((Expendables)itemType); }
    }
    protected override void Init()
    {
        base.Init();
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

        _button1 = get<Button>((int)Buttons.Button1).gameObject;
        _button2 = get<Button>((int)Buttons.Button2).gameObject;

        _text1 = get<TextMeshProUGUI>((int)TMPro.Text1);
        _text2 = get<TextMeshProUGUI>((int)TMPro.Text2);

        AddUIEvent(exit, (PointerEventData data) => { ClosePopup(); }, Define.UIEvent.Click);

        AddUIEvent(background.gameObject, (PointerEventData data) => { ClosePopup(); }, Define.UIEvent.Click);

        sprites = Managers.Resource.LoadAll<Sprite>("sprites/Icon/Stat");

        var item = _item.ConvertTo(_item.GetType());
        string sceneName = SceneManager.GetActiveScene().name;

        if (_item.GetType() == Type.GetType("Equipment"))
        {
            SetEquipment((Equipment)item);

            AddUIEvent(_button1, (PointerEventData data) => { EquipmentManagement((Equipment)item); }, Define.UIEvent.Click);

            AddUIEvent(_button2, (PointerEventData data) => { Sale(); }, Define.UIEvent.Click);
        }
        else if (_item.GetType() == Type.GetType("Expendables"))
        {
            SetExpendables((Expendables)item);

            AddUIEvent(_button1, (PointerEventData data) => { Use(sceneName); }, Define.UIEvent.Click);

            _button2.gameObject.SetActive(false);
        }
        else { base.ClosePopup(); }

        itemImage.sprite = _sprite;
        itemName.text = $"{_item.itemName}";
        itemInformation.text = $"{_item.explanation}";
    }
    private void SetEquipment(Equipment equipment)
    {
        int index = 0;
        string statType = "";

        for (int i = 0; i < 7; i++)
        {
            if (equipment[i] != null)
            {
                switch (i)
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

                if (statType != "Penetrate") { statTexts[index].text = $"{equipment[i]}"; }
                else { statTexts[index].text = ""; }

                statImages[index].sprite = Array.Find(sprites, sprite => sprite.name == statType);

                index++;
            }
        }

        while (index < 3) { statTexts[index++].gameObject.transform.parent.gameObject.SetActive(false); }

        if (_isEquipped) { _text1.text = "해제"; }
        else { _text1.text = "장착"; }

        _text2.text = "판매";

        if (_count == 1 && _isEquipped) { _button2.SetActive(false); }
        else { _button2.SetActive(true); }
    }
    private void SetExpendables(Expendables expendables)
    {
        int index = 0;
        string statType = "";

        for (int i = 0; i < 9; i++)
        {
            if (expendables[i] != 0)
            {
                switch (i)
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

        while (index < 3) { statTexts[index++].gameObject.transform.parent.gameObject.SetActive(false); }

        if(SceneManager.GetActiveScene().name == "Main") { _text1.text = "판매"; }
        else if(SceneManager.GetActiveScene().name == "InGame") { _text1.text = "사용"; }
    }
    private void EquipmentManagement(Equipment item)
    {
        _isEquipped = !_isEquipped;

        Managers.Data.InventoryEdit(_item.itemName, 0, _isEquipped ? 1 : 0);

        if (_count == 1 && _isEquipped) { _button2.SetActive(false); }
        else { _button2.SetActive(true); }

        SetEquipment(item);
    }
    private void Sale()
    {
        Managers.UI.ShowPopupUI<Sale_UI>("Sale");

        if (_isEquipped)
        {
            if (_count - 1 >= 1) { Managers.UI.PopupStack.Peek().GetComponent<Sale_UI>().Set(_item.itemName, _count - 1, true); }
        }
        else { Managers.UI.PopupStack.Peek().GetComponent<Sale_UI>().Set(_item.itemName, _count); }
    }
    private void Use(string sceneName)
    {
        if (sceneName == "Main")
        {
            Managers.UI.ShowPopupUI<Sale_UI>("Sale");
            Managers.UI.PopupStack.Peek().GetComponent<Sale_UI>().Set(_item.itemName, _count);
        }
        else if (sceneName == "InGame")
        {
            // increase stats
            if (_count - 1 == 0) { Managers.Data.InventoryEdit(_item.itemName, 0, 0, true); }
            else { Managers.Data.InventoryEdit(_item.itemName, -1); }

            base.ClosePopup();
        }
    }
}