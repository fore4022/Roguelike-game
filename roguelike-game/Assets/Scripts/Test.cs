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

        Managers.UI.showSceneUI<SwipeMenu_UI>("SwipeMenu");

        Managers.Data.inventory_edit("ItemID", 0);

        Debug.Log(Managers.Data.inventory);
        Debug.Log(Managers.Data.user);
    }
}
