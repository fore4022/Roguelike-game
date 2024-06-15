using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UI_Popup : UI_Base
{
    protected override void init() { Managers.UI.setCanvase(gameObject, true); }
    protected virtual void closePopup() { Managers.UI.closePopupUI(); }
}
