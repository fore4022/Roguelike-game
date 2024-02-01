using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    ObjectPool objectPool = new();
    private void Start()
    {
        StartCoroutine("monsterSpawn");

    }
    private void Update()
    {
    }
    IEnumerator monsterSpawn()
    {
        while(true)
        {
            objectPool.CreateObjects("zombie", 1);
            objectPool.activateObject("zombie", 1);
            yield return new WaitForSeconds(0.75f);
        }
    }
}
