using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Game_Manager
{
    public Player_Controller playerController;
    public List<GameObject> monsters = new List<GameObject>();
    public Map_Theme map;
    public int userGold;
    public int userExp;
    private void init()
    {
        map = Managers.Resource.load<Map_Theme>("Data/Map_Theme/zombie");
        if (playerController == null)
        {
            GameObject go = new GameObject { name = "Player" };
            go.AddComponent<Player_Controller>();
            playerController = go.GetComponent<Player_Controller>();
        }
    }
    public void stageStart()
    {
        if(map != null) { init(); }
    }
    public void stageEnd()
    {
        userGold += playerController.Gold;
        userExp += playerController.Exp;
        Time.timeScale = 0f;
    }
}