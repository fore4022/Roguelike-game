using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UI_Scene : UI_Base
{
    protected override void init() { Managers.UI.setCanvase(gameObject, true); }
    protected virtual void closeScene() { Managers.UI.closeSceneUI(); }
}
