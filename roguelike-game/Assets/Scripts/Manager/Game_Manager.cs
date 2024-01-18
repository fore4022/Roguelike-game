using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Game_Manager
{
    private Player_Controller playerController;
    public Player_Controller PlayerController { get { init();  return playerController; } }
    public int gold;
    public int exp;
    private void init()
    {
        if(playerController == null)
        {
            GameObject go = GameObject.Find("Player");
            playerController = go.GetComponent<Player_Controller>();
        }
        else
        {
            if(playerController.Hp <= 0)
            {
                stageEnd();
            }
        }
    }
    public void stageStart()
    {

    }
    public void stageEnd()
    {
        gold += playerController.Gold;
        exp += playerController.Exp;
        Time.timeScale = 0f;
    }
}
