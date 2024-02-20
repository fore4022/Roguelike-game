using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Map_Controller : MonoBehaviour
{
    private Vector3 z = new Vector3(0, 0, 1);
    private void Start()
    {
        transform.localScale = new Vector3(Managers.Game.camera_h, Managers.Game.camera_v);
        transform.position = Managers.Game.player.gameObject.transform.position - z;
    }
    private void Update()
    {
        if()
    }
}
