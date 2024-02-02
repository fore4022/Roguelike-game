using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
public class ObjectPool
{
    public Dictionary<String, Queue<GameObject>> boids = new Dictionary<String, Queue<GameObject>>();
    private GameObject objectPool;
    private GameObject monster;
    private void init()
    {
        objectPool = new GameObject { name = "@ObjectPool" };
        monster = new GameObject { name = "@Monster" };
    }
    public void CreateObjects(string prefabName, int count)
    {
        objectPool = GameObject.Find("@ObjectPool");
        monster = GameObject.Find("@Monster");
        if (objectPool == null || monster == null) { init(); }
        Queue<GameObject> queue;
        if (boids.ContainsKey(prefabName))
        {
            queue = boids[prefabName];
            for (int i = 0; i < count; i++)
            {
                GameObject go = Managers.Resource.instantiate($"Monster/{prefabName}", objectPool.transform);
                go.SetActive(false);
                queue.Enqueue(go);
            }
        }
        else
        {
            queue = new Queue<GameObject>();
            for(int i = 0; i < count; i++)
            {
                GameObject go = Managers.Resource.instantiate($"Monster/{prefabName}", objectPool.transform);
                go.SetActive(false);
                queue.Enqueue(go);
            }
            boids.Add(prefabName, queue);
        }
        boids[prefabName] = queue;
    }
    public void activateObject(string prefabName, int count)
    {
        Queue<GameObject> queue = boids[prefabName];
        for(int i = 0; i < count; i++)
        {
            GameObject go = queue.Dequeue();
            go.SetActive(true);
            go.transform.SetParent(monster.transform);
            Managers.Game.monsters.Add(go);
        }
        boids[prefabName] = queue;
    }
    public void disableObject(string prefabName, GameObject go)
    {
        Queue<GameObject> queue = boids[prefabName];
        go.SetActive(false);
        go.transform.SetParent(objectPool.transform);
        queue.Enqueue(go);
        boids[prefabName] = queue;
    }
}