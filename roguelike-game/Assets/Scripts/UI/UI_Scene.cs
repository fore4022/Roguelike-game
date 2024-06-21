public class UI_Scene : UI_Base
{
    protected override void Init() { Managers.UI.SetCanvase(gameObject, true); }
    protected virtual void CloseScene() { Managers.UI.CloseSceneUI(); }
}
