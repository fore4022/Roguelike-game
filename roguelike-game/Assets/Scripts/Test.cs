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

        Managers.Data.inventory_edit("weapon1", 1);
        Managers.Data.inventory_edit("weapon2", 1);
        Managers.Data.inventory_edit("weapon3", 1);
        Managers.Data.inventory_edit("weapon4", 1);
        Managers.Data.inventory_edit("weapon5", 1);
        Managers.Data.inventory_edit("weapon6", 1);
        Managers.Data.inventory_edit("weapon7", 1);
        Managers.Data.inventory_edit("weapon8", 1);
        Managers.Data.inventory_edit("weapon9", 1);
        Managers.Data.inventory_edit("weapon10", 1);

        Managers.UI.showSceneUI<SwipeMenu_UI>("SwipeMenu");
    }
}
