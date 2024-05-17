using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using UnityEngine.Events;
using System.Linq;
using Debug = UnityEngine.Debug;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour, IPointerClickHandler
{
    private void Start()
    {
        //Managers.Game.stageStart("Ground");
        //GameObject go = GameObject.Find("@Skill");
        //go.AddComponent<FireBall_Cast>();

        //Managers.UI.showSceneUI<SwipeMenu_UI>("SwipeMenu");
    }

    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("asdf");
    }
}
