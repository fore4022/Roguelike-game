using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public abstract class UI_Base : Util
{
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();
    protected abstract void init();
    protected void bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);
        for(int i = 0; i< names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject)) { objects[i] = FindChild(gameObject, names[i], true); }
            else { objects[i] = FindChild<T>(gameObject, names[i], true); }
        }
    }
    public T get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false) { return null; }
        return objects[idx] as T;
    }
    public static void AddUIEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = getOrAddComponent<UI_EventHandler>(go);
        switch(type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
            case Define.UIEvent.Enter:
                evt.OnPointerEnterHandler -= action;
                evt.OnPointerEnterHandler += action;
                break;
            case Define.UIEvent.Exit:
                evt.OnPointerExitHandler -= action;
                evt.OnPointerExitHandler += action;
                break;
            case Define.UIEvent.Down:
                evt.OnPointerDownHandler -= action;
                evt.OnPointerDownHandler += action;
                break;
            case Define.UIEvent.Up:
                evt.OnPointerUpHandler -= action;
                evt.OnPointerUpHandler += action;
                break;
        }
    }
}
