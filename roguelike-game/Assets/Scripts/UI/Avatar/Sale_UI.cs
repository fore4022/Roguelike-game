using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Sale_UI : UI_Popup
{
    private string _itemName;
    private int _countValue;
    private bool _isEquipped;
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
    public void Set(string itemName, int count, bool isEquipped = false)
    {
        _itemName = itemName;
        _countValue = count;
        _isEquipped = isEquipped;

        Init();
    }
    protected override void Init()
    {
        base.Init();
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
            if((int)count.value == 0) { return; }

            Managers.UI.PopupStack.Pop();

            Managers.Data.InventoryEdit(_itemName, -(int)count.value, (_isEquipped ? 1 : 0), (_countValue - (int)count.value) <= 0 && !_isEquipped ? true : false);

            if (!_isEquipped && _countValue - (int)count.value <= 0) { Managers.UI.ClosePopupUI(); }

            Destroy(this.gameObject);
        }, Define.UIEvent.Click);

        AddUIEvent(cancel, (PointerEventData data) => { ClosePopup(); }, Define.UIEvent.Click);

        AddUIEvent(count.gameObject, (PointerEventData data) =>
        {
            reconfirm.text = $"정말로 {_itemName} {(int)count.value}개를 파시겠습니까?";
            price.text = $"{50 * (int)count.value}";
        }, Define.UIEvent.Drag);
        AddUIEvent(count.gameObject, (PointerEventData data) =>
        {
            reconfirm.text = $"정말로 {_itemName} {(int)count.value}개를 파시겠습니까?";
            price.text = $"{50 * (int)count.value}";
        }, Define.UIEvent.EndDrag);

        count.maxValue = _countValue;
        count.value = Mathf.Max((int)count.maxValue / 2, 1);

        reconfirm.text = $"정말로 {_itemName} {(int)count.value}개를 파시겠습니까?";
        price.text = $"{50 * (int)count.value}";
    }
}
