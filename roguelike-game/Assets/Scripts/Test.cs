using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    private void Start()
    {
        Managers.Game.stageStart();
        StartCoroutine("monsterSpawn");
    }
    private void Update()
    {
    }
    IEnumerator monsterSpawn()
    {
        while(true)
        {
            Managers.Game.objectPool.CreateObjects("zombie","Monster_Controller", 1);
            Vector3 position = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4));
            Managers.Game.objectPool.activateObject("zombie", 1, position);
            yield return new WaitForSeconds(0.75f);
        }
    }
}
