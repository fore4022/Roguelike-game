using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Test : MonoBehaviour
{
    private void Start()
    {
        Managers.Data.inventory_edit("adsf", 120);

        Managers.Data.inventory_edit("asdf", 60);

        //Managers.Game.stageStart("Ground");
        //GameObject go = GameObject.Find("@Skill");
        //go.AddComponent<FireBall_Cast>();

        Managers.UI.showSceneUI<SwipeMenu_UI>("SwipeMenu");
    }
}
