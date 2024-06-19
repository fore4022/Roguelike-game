using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Sale_UI : UI_Popup
{
    private string itemName;
    private int countValue;
    private bool isEquipped;
    enum Buttons
    {
        Check,
        Cancel
    }
    enum Sliders
    {
        Count
    }
    enum TMPro
    {
        Reconfirm,
        Price
    }
    public void set(string _itemName, int _count, bool _isEquipped = false)
    {
        itemName = _itemName;
        countValue = _count;
        isEquipped = _isEquipped;

        init();
    }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Slider>(typeof(Sliders));
        bind<TextMeshProUGUI>(typeof(TMPro));

        GameObject check = get<Button>((int)Buttons.Check).gameObject;
        GameObject cancel = get<Button>((int)Buttons.Cancel).gameObject;

        Slider count = get<Slider>((int)Sliders.Count);

        TextMeshProUGUI reconfirm = get<TextMeshProUGUI>((int)TMPro.Reconfirm);
        TextMeshProUGUI price = get<TextMeshProUGUI>((int)TMPro.Price);

        AddUIEvent(check, (PointerEventData data) => 
        {
            Managers.UI.PopupStack.Pop();

            Managers.Data.inventory_edit(itemName, -(int)count.value, (isEquipped ? 1 : 0), (countValue - (int)count.value) <= 0 && !isEquipped ? true : false);

            if (!isEquipped && countValue - (int)count.value <= 0) { Managers.UI.closePopupUI(); }

            Destroy(this.gameObject);
        }, Define.UIEvent.Click);

        AddUIEvent(cancel, (PointerEventData data) => { closePopup(); }, Define.UIEvent.Click);

        AddUIEvent(count.gameObject, (PointerEventData data) =>
        {
            reconfirm.text = $"정말로 {itemName} {(int)count.value}개를 만큼 파시겠습니까?";
            price.text = $"{50 * (int)count.value}";
        }, Define.UIEvent.Drag);
        AddUIEvent(count.gameObject, (PointerEventData data) =>
        {
            reconfirm.text = $"정말로 {itemName} {(int)count.value}개를 만큼 파시겠습니까?";
            price.text = $"{50 * (int)count.value}";
        }, Define.UIEvent.EndDrag);

        count.maxValue = countValue;
        count.value = (int)count.maxValue / 2;

        reconfirm.text = $"정말로 {itemName} {(int)count.value}개를 만큼 파시겠습니까?";
        price.text = $"{50 * (int)count.value}";
    }
}
