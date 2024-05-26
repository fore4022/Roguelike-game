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
        Debug.Log("asdf");

        Managers.UI.showSceneUI<SwipeMenu_UI>("SwipeMenu");

        Debug.Log(Managers.Data.inventoryReader);
        Debug.Log(Managers.Data.userReader);
    }
}
