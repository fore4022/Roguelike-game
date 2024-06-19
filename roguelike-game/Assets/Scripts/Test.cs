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

        //Managers.Data.inventory_edit("weapon1", 1, 0);    //Equipment
        //Managers.Data.inventory_edit("weapon2", 1, 0);
        //Managers.Data.inventory_edit("weapon3", 1, 0);
        //Managers.Data.inventory_edit("weapon4", 1, 0);
        //Managers.Data.inventory_edit("weapon5", 1, 0);
        //Managers.Data.inventory_edit("weapon6", 1, 0);
        //Managers.Data.inventory_edit("weapon7", 1, 0);

        //Managers.Data.inventory_edit("item1", 1, 0);  //Expendables
        //Managers.Data.inventory_edit("item2", 2, 0);
        //Managers.Data.inventory_edit("item3", 3, 0);
        //Managers.Data.inventory_edit("item4", 4, 0);
        //Managers.Data.inventory_edit("item5", 5, 0);
        //Managers.Data.inventory_edit("item6", 6, 0);
        //Managers.Data.inventory_edit("item7", 7, 0);
        //Managers.Data.inventory_edit("item8", 8, 0);

        //Managers.UI.showSceneUI<SwipeMenu_UI>("SwipeMenu");

        Managers.UI.showSceneUI<Inventory_UI>("Inventory");
    }
}