using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnMonster : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(monsterSpawn());
    }
    IEnumerator monsterSpawn()
    {
        while(true)
        {
            int rand = UnityEngine.Random.Range(0, Managers.Game.map.monsterType.Count);
            Managers.Game.objectPool.activateObject(Managers.Game.map.monsterType[rand].ToString());
            yield return new WaitForSeconds(0.5f);
        }
    }
}
