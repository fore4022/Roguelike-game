using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Map_Controller : MonoBehaviour
{
    private Transform player;
    private Vector3 z = new Vector3(0, 0, 1);
    private void Start()
    {
        player = Managers.Game.player.gameObject.transform;
        transform.localScale = new Vector3(Managers.Game.camera_v, Managers.Game.camera_v);
        transform.position = Managers.Game.player.gameObject.transform.position + z;
    }
    private void Update()
    {
        if(player.position.x > 0)
        {
            if(player.position.x > transform.position.x + Managers.Game.camera_h)
            {
                transform.position = new Vector3(player.position.x, player.position.y) + z;
                return;
            }
        }
        else if(player.position.x < 0)
        {
            if(player.position.x < transform.position.x - Managers.Game.camera_h)
            {
                transform.position = new Vector3(player.position.x, player.position.y) + z;
                return;
            }
        }
        if(player.position.y > 0)
        {
            if(player.position.y > transform.position.y + Managers.Game.camera_v)
            {
                transform.position = new Vector3(player.position.x, player.position.y) + z;
            }
        }
        else if(player.position.y < 0)
        {
            if(player.position.y < transform.position.y - Managers.Game.camera_v)
            {
                transform.position = new Vector3(player.position.x, player.position.y) + z;
            }
        }
    }
}
