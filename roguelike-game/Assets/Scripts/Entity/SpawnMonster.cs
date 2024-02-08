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
        float seconds = Managers.Game.creationCycle;
        while (Managers.Game.isSpawn)
        {
            if (((Managers.Game.minute % 8 == 0) && (Managers.Game.second % 8 == 0)) || Managers.Game.minute % 25 == 0) { Managers.Game.inBattle = true; }
            if (!Managers.Game.inBattle)
            {
                int rand = UnityEngine.Random.Range(0, Managers.Game.map.monsterType.Count);
                Managers.Game.objectPool.activateObject(Managers.Game.map.monsterType[rand].ToString());
            }
            else { Managers.Game.stopWatch.Stop(); }
            if(seconds < 0.1f) { seconds = 0.1f; }
            else { seconds = Managers.Game.creationCycle - ((0.6f / 60) * Managers.Game.minute); }
            yield return new WaitForSeconds(seconds);
        }
    }
}
