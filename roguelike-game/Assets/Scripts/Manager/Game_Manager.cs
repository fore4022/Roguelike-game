using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using UnityEngine.Events;
using System;
using System.Linq;
using System.Threading;
using UnityEditor.Compilation;
using Debug = UnityEngine.Debug;
public class Game_Manager
{
    public Player_Controller player;
    public ObjectPool objectPool = new();
    public Stopwatch stopWatch = new();
    public SpawnMonster spawnMonster;
    public List<Skill> skills;
    public Map_Theme map;
    public float minute { get { return stopWatch.Elapsed.Minutes; } }
    public float second { get { return stopWatch.Elapsed.Seconds; } }
    public float milliSeconds { get { return stopWatch.Elapsed.Milliseconds; } }
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
        creationCycle = 1f;
        isSpawn = true;
        inBattle = false;
        killCount = 0;
        map = Managers.Resource.load<Map_Theme>($"Data/Map_Theme/{Theme}");
        GameObject go = GameObject.Find("Player");
        if (skills == null) { skills = Managers.Resource.LoadAll<Skill>("Data/Skill/").ToList<Skill>(); }
        if (go == null)
        {
            go = Managers.Resource.instantiate("Prefab/Player", null);
            player = go.AddComponent<Player_Controller>();
        }
        if (GameObject.Find("@Monster") == null) { go = new GameObject { name = "@Monster" }; }
        spawnMonster = Util.getOrAddComponent<SpawnMonster>(go);
        if (GameObject.Find("@Skill") == null) { go = new GameObject { name = "@Skill" }; }  
        player.updateStatus += increaseKillCount;
    }
    public void stageStart(string Theme)
    {
        init(Theme);
        objectPool.init();
        foreach (string str in map.monsterType) { objectPool.createObjects(typeof(Monster_Controller), str, 1200); }
        //foreach (Skill skill in skills) { objectPool.createObjects(typeof(Base_Skill), skill.skillName, 20); }
        stopWatch.Start();
        isSpawn = true;
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
        userGold += (int)player.Gold;
        userExp += (int)player.Exp;
    }
}