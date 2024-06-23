using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Debug = UnityEngine.Debug;
public class Game_Manager
{
    public List<Skill> skills;

    public Player_Controller player;
    public ObjectPool objectPool = new();
    public SpawnMonster spawnMonster;
    public Map_Theme map;
    public GameObject skill;
    public Item item;

    public float camera_h { get { return Camera.main.orthographicSize * 2; } }
    public float camera_w { get { return Camera.main.orthographicSize * 2 * Camera.main.aspect; } }

    public float basicExp = 55;
    public float creationCycle;
    public float timer;

    public int userGold;
    public int userExp;

    public bool isSpawn;

    private void Init(string Theme)
    {
        creationCycle = 1f;

        isSpawn = true;

        map = Managers.Resource.Load<Map_Theme>($"Data/Map_Theme/{Theme}");
        skill = GameObject.Find("@Skill");

        GameObject go = GameObject.Find("Player");

        if (go == null)
        {
            go = Managers.Resource.Instantiate("Prefab/Player", null);
            go.transform.position = Vector3.zero;

            player = go.AddComponent<Player_Controller>();
        }

        go = GameObject.Find("Main Camera");
        go.AddComponent<Main_Camera>();


        if (GameObject.Find("@Monster") == null) { go = new GameObject { name = "@Monster" }; }

        spawnMonster = Util.GetOrAddComponent<SpawnMonster>(go);

        if (skills == null) { skills = Managers.Resource.LoadAll<Skill>("Data/Skill/").ToList<Skill>(); }

        //go = Managers.Resource.instantiate("Prefab/Map");
    }
    public void StageStart(string Theme)
    {
        Init(Theme);
        objectPool.Init();

        foreach (string str in map.monsterType) { objectPool.CreateObjects(typeof(Monster_Controller), str, 600); }

        foreach (Skill skill in skills) { objectPool.CreateObjects(typeof(Base_SkillCast), /*skill.skillName*/"FireBall", 20); }

        isSpawn = true;

        Managers.UI.ShowSceneUI<Status_UI>("Status");
        Managers.UI.ShowSceneUI<Timer_UI>("Timer");
        Managers.UI.ShowSceneUI<Pause_UI>("Pause");
        Managers.UI.ShowPopupUI<Controller_UI>("Controller");

        spawnMonster.StartCoroutine(spawnMonster.Spawn());
    }
    public void StageEnd()
    {
        isSpawn = false;

        userGold += (int)player.Gold;
        userExp += (int)player.Exp;

        Time.timeScale = 0f;
    }
}