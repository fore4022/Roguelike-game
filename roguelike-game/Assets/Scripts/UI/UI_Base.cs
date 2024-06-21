using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class UI_Base : Util
{
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();
    protected abstract void Init();
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
}
