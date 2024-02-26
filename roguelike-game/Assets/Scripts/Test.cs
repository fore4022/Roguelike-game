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
        //go.AddComponent<BloodMagicBullet_Cast>();
        //go.AddComponent<BloodyMassacre_Cast>();
        go.AddComponent<CorpseExplosion_Cast>();
    }
    private void Update()
    {

    }
}
