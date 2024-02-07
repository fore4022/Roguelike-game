using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using UnityEngine.Events;
public class Game_Manager
{
    public Player_Controller player;
    public List<GameObject> monsters = new();
    public SpawnMonster spawnMonster = new();
    public ObjectPool objectPool = new();
    public Stopwatch stopWatch = new();
    public Map_Theme map;
    public float stopWatchMinute { get { return stopWatch.Elapsed.Minutes; } }
    public float stopWatchSecond { get { return stopWatch.Elapsed.Seconds; } }
    public float creationCycle;
    public float timer;
    public float basicExp = 75;
    public float increaseExp = 100;
    public int userGold;
    public int userExp;
    public int killCount;
    public bool isSpawn;
    public bool inBattle;
    private void init(string Theme)
    {
        creationCycle = 0.7f;
        isSpawn = false;
        inBattle = false;
        killCount = 0;
        map = Managers.Resource.load<Map_Theme>($"Data/Map_Theme/{Theme}");
        GameObject go = GameObject.Find("Player");
        if (go == null)
        {
            go = Managers.Resource.instantiate("Prefab/Player", null);
            player = go.AddComponent<Player_Controller>();
        }
        player.updateStatus += increaseKillCount;
    }
    public void stageStart(string Theme)
    {
        init(Theme);
        objectPool.init();
        foreach(string str in map.monsterType)
        {
            objectPool.CreateObjects(str, 1200);
        }
        stopWatch.Start();
        isSpawn = true;
        Time.timeScale = 1f;
        spawnMonster.StartCoroutine(spawnMonster.Spawn());
    }
    public void increaseKillCount()
    {
        killCount++;
        if(player.Hp != player.MaxHp)
        {
            if (killCount % 20 == 0) { player.Hp += player.MaxHp / 1000; }
        }
    }
    public void stageEnd()
    {
        stopWatch.Stop();
        isSpawn = false;
        Time.timeScale = 0f;
        userGold += (int)player.Gold;
        userExp += (int)player.Exp;
    }
}