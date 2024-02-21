using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Main_Camera : MonoBehaviour
{
    private void Update()
    {
        if (!Managers.Game.player) { return; }
        transform.position = new Vector3(Managers.Game.player.gameObject.transform.position.x, Managers.Game.player.gameObject.transform.position.y, -1);
    }
}
