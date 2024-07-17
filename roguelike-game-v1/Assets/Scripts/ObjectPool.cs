using System.Collections.Generic;
using UnityEngine;
public class ObjectPool
{
    public Dictionary<string, Queue<GameObject>> boids = new();
    public Dictionary<string, Monster> monsterData = new();

    private GameObject _objectPool;
    private GameObject _skill;
    public void Init()
    {
        if (GameObject.Find("@Skill") == null) { _skill = new GameObject { name = "@Skill" }; }

        if (GameObject.Find("@ObjectPool") == null) { _objectPool = new GameObject { name = "@ObjectPool" }; }

        foreach (string str in Managers.Game.map.monsterType) { monsterData.Add(str, Managers.Resource.Load<Monster>($"Data/Monster/{str}")); }
    }
    public void CreateObjects(System.Type baseType, string prefabName, int count, string scriptName = null)
    {
        Queue<GameObject> queue;

        string folderName = null;

        if(count == 0) { return; }

        if(baseType == typeof(Monster_Controller)) { folderName = "Monster"; }
        else if(baseType == typeof(Base_SkillCast)) { folderName = "Skill"; }

        if (folderName == null) { return; }

        string path = $"Prefab/{folderName}/{prefabName}";

        GameObject prefab = Managers.Resource.Load<GameObject>(path);

        if (boids.ContainsKey(prefabName))
        {
            queue = boids[prefabName];

            for (int i = 0; i < count; i++)
            {
                GameObject go = GameObject.Instantiate(prefab, _objectPool.transform);

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
                GameObject go = GameObject.Instantiate(prefab, _objectPool.transform);

                go.SetActive(false);
                queue.Enqueue(go);
            }

            boids.Add(prefabName, queue);
        }
    }
    public GameObject ActivateObject(System.Type baseType, string prefabName)
    {
        Queue<GameObject> queue = boids[prefabName];
        GameObject go = queue.Dequeue();

        go.SetActive(true);

        if (baseType == typeof(Monster_Controller)) { go.transform.SetParent(Managers.Game.spawnMonster.gameObject.transform); }
        else if (baseType == typeof(Base_SkillCast)) { go.transform.SetParent(_skill.transform); }

        boids[prefabName] = queue;

        return go;
    }
    public void DisableObject(string prefabName, GameObject go)
    {
        Queue<GameObject> queue = boids[prefabName];

        go.transform.SetParent(_objectPool.transform);
        go.SetActive(false);
        queue.Enqueue(go);

        boids[prefabName] = queue;
    }
}