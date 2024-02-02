using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor.Compilation;
using UnityEngine;
public class ObjectPool
{
    public Dictionary<string, Queue<GameObject>> boids = new Dictionary<string, Queue<GameObject>>();
    public Dictionary<string, Monster> monsterData = new Dictionary<string, Monster>();
    private GameObject objectPool;
    private GameObject monster;
    private void init()
    {
        if (GameObject.Find("@ObjectPool") == null) { objectPool = new GameObject { name = "@ObjectPool" }; }
        
        if (GameObject.Find("@Monster") == null) { monster = new GameObject { name = "@Monster" }; }
        foreach (string str in Managers.Game.map.monsterType) { monsterData.Add(str, Managers.Resource.load<Monster>($"Data/Monster/{str}")); }
    }
    public void CreateObjects(string prefabName, string scriptName, int count)
    {
        init();
        Queue<GameObject> queue;
        if (boids.ContainsKey(prefabName))
        {
            queue = boids[prefabName];
            for (int i = 0; i < count; i++)
            {
                GameObject go = Managers.Resource.instantiate($"Prefab/Monster/{prefabName}", objectPool.transform);
                go.SetActive(false);
                queue.Enqueue(go);
            }
            boids[prefabName] = queue;
        }
        else
        {
            queue = new Queue<GameObject>();
            for(int i = 0; i < count; i++)
            {
                GameObject go = Managers.Resource.instantiate($"Prefab/Monster/{prefabName}", objectPool.transform);
                go.SetActive(false);
                queue.Enqueue(go);
            }
            boids.Add(prefabName, queue);
        }
        System.Type scriptType = System.Type.GetType(scriptName);
        foreach (GameObject go in boids[prefabName])
        {
            Monster_Controller script = go.AddComponent(scriptType) as Monster_Controller;
            script.monsterType = monsterData[prefabName];
        }
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