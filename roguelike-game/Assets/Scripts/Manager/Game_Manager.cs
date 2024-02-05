using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Game_Manager
{
    public Player_Controller playerController;
    public List<GameObject> monsters = new List<GameObject>();
    public ObjectPool objectPool = new();
    public Map_Theme map;
    public bool isSpawn = false;
    public int userGold;
    public int userExp;
    private void init()
    {
        isSpawn = false;
        map = Managers.Resource.load<Map_Theme>("Data/Map_Theme/zombie");
        Debug.Log(map.monsterType.Count);
        GameObject go = GameObject.Find("Player");
        if (go == null)
        {
            go = Managers.Resource.instantiate("Prefab/Player", null);
            playerController = go.AddComponent<Player_Controller>();
        }
    }
    public void stageStart()
    {
        init();
        objectPool.init();
        isSpawn = true;
    }
    public void stageEnd()
    {
        userGold += playerController.Gold;
        userExp += playerController.Exp;
        Time.timeScale = 0f;
    }
}