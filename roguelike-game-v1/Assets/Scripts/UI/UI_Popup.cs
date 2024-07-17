public class UI_Popup : UI_Base
{
    protected override void Init() { Managers.UI.SetCanvase(gameObject, true); }
    protected virtual void ClosePopup() { Managers.UI.ClosePopupUI(); }
}
