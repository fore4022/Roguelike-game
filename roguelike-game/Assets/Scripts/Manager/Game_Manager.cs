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
    public int c;//

    public List<Skill> skills;

    public Player_Controller player;
    public ObjectPool objectPool = new();
    public SpawnMonster spawnMonster;
    public Map_Theme map;
    public GameObject skill;
    public Item item;

    public Stopwatch stopWatch = new();

    public float minute { get { return stopWatch.Elapsed.Minutes; } }
    public float second { get { return stopWatch.Elapsed.Seconds; } }

    public float camera_h { get { return Camera.main.orthographicSize * 2; } }
    public float camera_w { get { return Camera.main.orthographicSize * 2 * Camera.main.aspect; } }

    public float basicExp = 55;
    public float creationCycle;
    public float timer;

    public int userGold;
    public int userExp;

    public bool isSpawn;
    public bool inBattle;
    private void init(string Theme)
    {
        creationCycle = 1f;
        isSpawn = true;
        inBattle = false;

        map = Managers.Resource.load<Map_Theme>($"Data/Map_Theme/{Theme}");
        skill = GameObject.Find("@Skill");
        GameObject go = GameObject.Find("Player");
        if (go == null)
        {
            go = Managers.Resource.instantiate("Prefab/Player", null);
            go.transform.position = Vector3.zero;
            player = go.AddComponent<Player_Controller>();
        }

        go = GameObject.Find("Main Camera");
        go.AddComponent<Main_Camera>();

        if (GameObject.Find("@Monster") == null) { go = new GameObject { name = "@Monster" }; }
        spawnMonster = Util.getOrAddComponent<SpawnMonster>(go);
        if (skills == null) { skills = Managers.Resource.LoadAll<Skill>("Data/Skill/").ToList<Skill>(); }

        //go = Managers.Resource.instantiate("Prefab/Map");
    }
    public void stageStart(string Theme)
    {
        init(Theme);
        objectPool.init();

        foreach (string str in map.monsterType) { objectPool.createObjects(typeof(Monster_Controller), str, 600); }
        foreach (Skill skill in skills) { objectPool.createObjects(typeof(Base_SkillCast), /*skill.skillName*/"FireBall", 20); }

        stopWatch.Start();
        isSpawn = true;

        Managers.UI.showSceneUI<Status_UI>("Status");
        Managers.UI.showSceneUI<Timer_UI>("Timer");
        Managers.UI.showSceneUI<Pause_UI>("Pause");
        Managers.UI.showPopupUI<Controller_UI>("Controller");

        spawnMonster.StartCoroutine(spawnMonster.Spawn());
    }
    public void stageEnd()
    {
        stopWatch.Stop();
        isSpawn = false;
        userGold += (int)player.Gold;
        userExp += (int)player.Exp;

        Time.timeScale = 0f;
    }
}