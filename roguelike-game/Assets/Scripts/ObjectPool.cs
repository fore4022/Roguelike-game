using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor.Compilation;
using UnityEngine;
public class ObjectPool
{
    public Dictionary<string, Queue<GameObject>> boids = new();
    public Dictionary<string, Monster> monsterData = new();
    private GameObject objectPool;
    private Transform player;
    public void init()
    {
        player = Managers.Game.player.gameObject.transform;
        if (GameObject.Find("@ObjectPool") == null) { objectPool = new GameObject { name = "@ObjectPool" }; }
        foreach (string str in Managers.Game.map.monsterType) { monsterData.Add(str, Managers.Resource.load<Monster>($"Data/Monster/{str}")); }
    }
    public void createObjects(System.Type type, string prefabName, int count, string scriptName = null)
    {
        Queue<GameObject> queue;
        string folderName = null;
        if(type == typeof(Monster_Controller)) { folderName = "Monster"; }
        else if(type == typeof(Base_Skill)) { folderName = "Skill"; }
        if (folderName == null) { return; }
        if (boids.ContainsKey(prefabName))
        {
            queue = boids[prefabName];
            for (int i = 0; i < count; i++)
            {
                GameObject go = Managers.Resource.instantiate($"Prefab/{folderName}/{prefabName}", objectPool.transform);
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
        System.Type scriptType = null;
        if (scriptName != null) { scriptType = System.Type.GetType(scriptName); }
        if (scriptType == null) { scriptType = System.Type.GetType("Monster_Controller"); }
        foreach (GameObject go in boids[prefabName])
        {
            Monster_Controller script = go.AddComponent(scriptType) as Monster_Controller;
            script.monsterType = monsterData[prefabName];
            //go.GetComponent<Animator>().runtimeAnimatorController = Managers.Resource.load<RuntimeAnimatorController>($"Animation/{prefabName}/{prefabName}");
        }
    }
    public void activateObject(string prefabName, int count = 0)
    {
        if (count < 0) { return; }
        else if(count == 1 || count == 0)
        {
            Queue<GameObject> queue = boids[prefabName];
            GameObject go = queue.Dequeue();
            go.SetActive(true);
            go.transform.SetParent(Managers.Game.spawnMonster.gameObject.transform);
            go.transform.position = position();
            Managers.Game.monsters.Add(go);
            boids[prefabName] = queue;
        }
        else { activateObjects(prefabName, count); }
    }
    private void activateObjects(string prefabName, int count)
    {
        Queue<GameObject> queue = boids[prefabName];
        count = count <= 12 ? count : 12;
        for (int i = 0; i < count; i++)
        {
            GameObject go = queue.Dequeue();
            go.SetActive(true);
            go.transform.SetParent(Managers.Game.spawnMonster.gameObject.transform);
            go.transform.position = position();
            Managers.Game.monsters.Add(go);
        }
        boids[prefabName] = queue;
    }
    private Vector3 position()
    {
        float x;
        float y;
        if (UnityEngine.Random.Range(0, 2) == 1)
        {
            x = UnityEngine.Random.Range(-7.5f, 7.51f) >= 0 ? 7.5f : -7.5f;
            y = UnityEngine.Random.Range(-11f, 11.1f);
        }
        else
        {
            y = UnityEngine.Random.Range(-11f, 11.1f) >= 0 ? -11f : 11f;
            x = UnityEngine.Random.Range(-7.5f, 7.51f);
        }
        return new Vector3(player.position.x + x, player.position.y + y, 0);
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