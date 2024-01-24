using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Manager
{
    int order = 0;
    public Stack<UI_Popup> PopupStack = new Stack<UI_Popup>();
    public Stack<UI_Scene> SceneStack = new Stack<UI_Scene>();
    public GameObject UI
    {
        get
        {
            GameObject UI = GameObject.Find("UI");
            if(UI == null)
            {
                UI = new GameObject { name = "UI" };
            }
            return UI;
        }
    }
    public void setCanvase(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.getOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;
        if(sort)
        {
            canvas.sortingOrder = (order++);
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }
    public void showSceneUI<T>(string name = null) where T : UI_Scene
    {
        if(string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }
        GameObject go = Managers.Resource.instantiate($"UI/{name}");
        T scene = Util.getOrAddComponent<T>(go);
        SceneStack.Push(scene);
        go.transform.SetParent(UI.transform);
        go.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
    }
    public void showPopupUI<T>(string name = null) where T : UI_Popup
    {
        if(string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }
        GameObject go = Managers.Resource.instantiate($"UI/{name}");
        T popup = Util.getOrAddComponent<T>(go);
        PopupStack.Push(popup);
        go.transform.SetParent(UI.transform);
        go.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
    }
    public void closeSceneUI()
    {
        if(SceneStack.Count == 0)
        {
            return;
        }
        UI_Scene go = SceneStack.Pop();
        Managers.Resource.destroy(go.gameObject);
        if(SceneStack.Count != 0)
        {
            SceneStack.Peek().gameObject.SetActive(true);
        }
    }
    public void closePopupUI()
    {
        if(PopupStack.Count == 0)
        {
            return;
        }
        UI_Popup go = PopupStack.Pop();
        Managers.Resource.destroy(go.gameObject);
    }
}
