using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Util : MonoBehaviour
{
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform != null) { return null; }
        return transform.gameObject;
    }
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : Object
    {
        if (go == null) { return null; }
        if(recursive == false)
        {
            for(int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);

                if(string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null) { return component; }
                }
            }
        }
        else
        {
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name) { return component; }
            }
        }
        return null;
    }
    public static T FindParent<T>(GameObject go, string name = null) where T : Object
    {
        for(; ; )
        {
            if(go.transform.parent != null)
            {
                go = go.transform.parent.gameObject;
                if(string.IsNullOrEmpty(name) || go.name == name)
                {
                    if (go.GetComponent<T>() != null) { return go.GetComponent<T>(); }
                }
            }
            else { return null; }
        }
    }
    public static T getOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null) { component = go.AddComponent<T>(); }
        return component;
    }
}
