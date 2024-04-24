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
    private GameObject skill;
    public void init()
    {
        if (GameObject.Find("@Skill") == null) { skill = new GameObject { name = "@Skill" }; }
        if (GameObject.Find("@ObjectPool") == null) { objectPool = new GameObject { name = "@ObjectPool" }; }
        foreach (string str in Managers.Game.map.monsterType) { monsterData.Add(str, Managers.Resource.load<Monster>($"Data/Monster/{str}")); }
    }
    public void createObjects(System.Type baseType, string prefabName, int count, string scriptName = null)
    {
        Queue<GameObject> queue;
        string folderName = null;
        if(count == 0) { return; }
        if(baseType == typeof(Monster_Controller)) { folderName = "Monster"; }
        else if(baseType == typeof(Base_SkillCast)) { folderName = "Skill"; }
        if (folderName == null) { return; }
        
        if (boids.ContainsKey(prefabName))
        {
            queue = boids[prefabName];
            for (int i = 0; i < count; i++)
            {
                GameObject go;
                if (folderName == "Monster") { go = Managers.Resource.instantiate($"Prefab/monster", objectPool.transform); }
                else { go = Managers.Resource.instantiate($"Prefab/{folderName}/{prefabName}", objectPool.transform); }
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
                GameObject go;
                if (folderName == "Monster") { go = Managers.Resource.instantiate($"Prefab/monster", objectPool.transform); }
                else { go = Managers.Resource.instantiate($"Prefab/{folderName}/{prefabName}", objectPool.transform); }
                go.SetActive(false);
                queue.Enqueue(go);
            }
            boids.Add(prefabName, queue);
        }

        System.Type scriptType = null;
        if (scriptName != null) { scriptType = System.Type.GetType(scriptName); }
        if(folderName == "Monster")
        {
            if (scriptType == null) { scriptType = System.Type.GetType("Monster_Controller"); }
            foreach (GameObject go in boids[prefabName])
            {
                Monster_Controller script = go.AddComponent(scriptType) as Monster_Controller;
                script.monsterType = monsterData[prefabName];
                go.AddComponent<Animator>().runtimeAnimatorController = Managers.Resource.load<RuntimeAnimatorController>($"Animation/Monster/{prefabName}/{prefabName}");
            }
        }
    }
    public GameObject activateObject(System.Type baseType, string prefabName)
    {
        Queue<GameObject> queue = boids[prefabName];
        GameObject go = queue.Dequeue();
        go.SetActive(true);
        if (baseType == typeof(Monster_Controller)) { go.transform.SetParent(Managers.Game.spawnMonster.gameObject.transform); }
        else if (baseType == typeof(Base_SkillCast)) { go.transform.SetParent(skill.transform); }
        boids[prefabName] = queue;
        return go;
    }
    public void disableObject(string prefabName, GameObject go)
    {
        Queue<GameObject> queue = boids[prefabName];
        go.transform.SetParent(objectPool.transform);
        go.SetActive(false);
        queue.Enqueue(go);
        boids[prefabName] = queue;
    }
}