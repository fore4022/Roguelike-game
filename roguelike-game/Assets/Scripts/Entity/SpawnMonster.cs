using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class SpawnMonster : MonoBehaviour
{
    private GameObject monster;
    public void init()
    {
        if (GameObject.Find("@Monster") == null) { monster = new GameObject { name = "@Monster" }; }
        monster.AddComponent<SpawnMonster>();
    }
    public IEnumerator Spawn()
    {
        while (Managers.Game.isSpawn)
        {
            if(!Managers.Game.inBattle)
            {
                int rand = UnityEngine.Random.Range(0, Managers.Game.map.monsterType.Count);
                Managers.Game.objectPool.activateObject(Managers.Game.map.monsterType[rand].ToString());
            }
            else
            {
                Managers.Game.stopWatch.Stop();
            }
            yield return new WaitForSeconds(Managers.Game.creationCycle - (0.6f / (60 - Managers.Game.stopWatchMinute)));
        }
    }
}
