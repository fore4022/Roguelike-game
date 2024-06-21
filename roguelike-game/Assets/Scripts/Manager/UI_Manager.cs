using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Manager
{
    public Stack<UI_Popup> PopupStack = new Stack<UI_Popup>();
    public Stack<UI_Scene> SceneStack = new Stack<UI_Scene>();

    private int _order = 0;

    public GameObject UI
    {
        get
        {
            GameObject UI = GameObject.Find("UI");

            if (UI == null) { UI = new GameObject { name = "UI" }; }

            return UI;
        }
    }
    public void SetCanvase(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);

        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort) { canvas.sortingOrder = (_order++); }
        else { canvas.sortingOrder = 0; }
    }
    public void ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name)) { name = typeof(T).Name; }

        GameObject go = Managers.Resource.Instantiate($"UI/{name}");

        T scene = Util.GetOrAddComponent<T>(go);

        SceneStack.Push(scene);

        go.transform.SetParent(UI.transform);
        go.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
    }
    public void ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name)) { name = typeof(T).Name; }

        GameObject go = Managers.Resource.Instantiate($"UI/{name}");

        T popup = Util.GetOrAddComponent<T>(go);

        PopupStack.Push(popup);

        go.transform.SetParent(UI.transform);
        go.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
    }
    public void CloseSceneUI()
    {
        if (SceneStack.Count == 0) { return; }

        UI_Scene go = SceneStack.Pop();

        Managers.Resource.Destroy(go.gameObject);

        if (SceneStack.Count != 0) { SceneStack.Peek().gameObject.SetActive(true); }
    }
    public void ClosePopupUI()
    {
        if (PopupStack.Count == 0) { return; }

        UI_Popup go = PopupStack.Pop();

        Managers.Resource.Destroy(go.gameObject);
    }
}