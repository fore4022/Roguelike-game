using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Resource_Manager
{
    public T load<T>(string path) where T : Object { return Resources.Load<T>(path); }
    public T[] LoadAll<T>(string path) where T : Object { return Resources.LoadAll<T>(path); }
    public GameObject instantiate(string path, Transform transform = null)
    {
        GameObject prefab = load<GameObject>($"{path}");
        if (prefab == null) { return null; }
        return Object.Instantiate(prefab, transform);
    }
    public void destroy(GameObject go)
    {
        if (go == null) { return; }
        Object.Destroy(go);
    }
}
