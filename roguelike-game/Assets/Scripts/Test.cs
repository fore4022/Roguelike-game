using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Test : MonoBehaviour
{
    private void Start()
    {
        //Managers.Game.stageStart("Ground");
        //GameObject go = GameObject.Find("@Skill");
        //go.AddComponent<FireBall_Cast>();

        //Managers.Data.inventory_edit("weapon1", 1, 0);
        //Managers.Data.inventory_edit("weapon2", 1, 0);
        //Managers.Data.inventory_edit("weapon3", 1, 0);
        //Managers.Data.inventory_edit("weapon4", 1, 0);
        //Managers.Data.inventory_edit("weapon5", 1, 0);
        //Managers.Data.inventory_edit("weapon6", 1, 0);
        //Managers.Data.inventory_edit("weapon7", 1, 0);

        Managers.UI.showSceneUI<SwipeMenu_UI>("SwipeMenu");

        //Managers.UI.showSceneUI<Inventory_UI>("Inventory");
    }
}