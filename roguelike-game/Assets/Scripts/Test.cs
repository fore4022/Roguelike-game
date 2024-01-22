using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    ObjectPool objectPool = new();
    private void Start()
    {
        objectPool.CreateObjects("zombie", 3);
        objectPool.activateObject("zombie", 2);
        objectPool.disableObject("zombie", Managers.Game.monsters[1]);
    }
}
