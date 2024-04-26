using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using UnityEngine.Events;
using System.Linq;
using Debug = UnityEngine.Debug;
public class Test : MonoBehaviour
{
    private void Start()
    {
        Managers.Game.stageStart("Ground");
        GameObject go = GameObject.Find("@Skill");
        go.AddComponent<FireBall_Cast>();
    }
    private void Update()
    {
        Debug.Log($"{DateTime.Now.Hour} : {DateTime.Now.Minute} : {DateTime.Now.Second}");
    }
}
