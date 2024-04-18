using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using UnityEngine.Events;
using System.Linq;
public class Test : MonoBehaviour
{
    private void Start()
    {
        Managers.Game.stageStart("Ground");
        GameObject go = GameObject.Find("@Skill");
        go.AddComponent<Ignition_Cast>();
        go.AddComponent<FireBall_Cast>();
    }
    private void Update()
    {

    }
}
