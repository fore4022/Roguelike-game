using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
public class SpawnMonster : MonoBehaviour
{
    public IEnumerator Spawn()
    {
        GameObject go;
        float seconds = Managers.Game.creationCycle;
        while (Managers.Game.isSpawn)
        {
            if (Managers.Game.minute != 0 && (((Managers.Game.minute % 8 == 0) && (Managers.Game.second % 8 == 0)) || Managers.Game.minute % 25 == 0)) { Managers.Game.inBattle = true; }
            if (!Managers.Game.inBattle)
            {
                int rand = UnityEngine.Random.Range(0, Managers.Game.map.monsterType.Count);
                go = Managers.Game.objectPool.activateObject(typeof(Monster_Controller), Managers.Game.map.monsterType[rand].ToString());
                go.transform.position = position();
            }
            else { Managers.Game.stopWatch.Stop(); }
            if(seconds < 0.1f) { seconds = 0.1f; }
            else { seconds = Managers.Game.creationCycle - ((0.6f / 60) * Managers.Game.minute); }
            yield return new WaitForSeconds(seconds);
        }
    }
    private Vector3 position()
    {
        float x;
        float y;
        if (UnityEngine.Random.Range(0, 2) == 1)
        {
            x = UnityEngine.Random.Range(-7.5f, 7.51f) >= 0 ? 7.5f : -7.5f;
            y = UnityEngine.Random.Range(-11f, 11.1f);
        }
        else
        {
            y = UnityEngine.Random.Range(-11f, 11.1f) >= 0 ? -11f : 11f;
            x = UnityEngine.Random.Range(-7.5f, 7.51f);
        }
        return new Vector3(Managers.Game.player.gameObject.transform.position.x + x, Managers.Game.player.gameObject.transform.position.y + y, 0);
    }
}
