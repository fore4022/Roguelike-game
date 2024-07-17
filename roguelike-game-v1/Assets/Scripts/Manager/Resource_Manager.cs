using UnityEngine;
public class Resource_Manager
{
    public T Load<T>(string path) where T : Object { return Resources.Load<T>(path); }
    public T[] LoadAll<T>(string path) where T : Object { return Resources.LoadAll<T>(path); }
    public GameObject Instantiate(string path, Transform transform = null)
    {
        GameObject prefab = Load<GameObject>($"{path}");

        if (prefab == null) { return null; }

        return Object.Instantiate(prefab, transform);
    }
    public void Destroy(GameObject go)
    {
        if (go == null) { return; }

        Object.Destroy(go);
    }
}
