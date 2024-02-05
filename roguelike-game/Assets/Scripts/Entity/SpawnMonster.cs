using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnMonster
{
    public void Spawn()
    {
        while (Managers.Game.isSpawn)
        {
            if(Managers.Game.stopWatch.Elapsed.Seconds % Managers.Game.creationCycle == 0)
            {
                int rand = UnityEngine.Random.Range(0, Managers.Game.map.monsterType.Count);
                Managers.Game.objectPool.activateObject(Managers.Game.map.monsterType[rand].ToString());
                Debug.Log("Asdf");
            }
        }
    }
}
