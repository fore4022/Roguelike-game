using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Map_Controller : MonoBehaviour
{
    private Vector3 z = new Vector3(0, 0, 1);
    private void Start()
    {
        transform.localScale = new Vector3(Managers.Game.camera_v, Managers.Game.camera_v);
        transform.position = Managers.Game.player.gameObject.transform.position + z;
    }
    private void Update()
    {
        if (Mathf.Abs(Managers.Game.player.gameObject.transform.position.x) > Mathf.Abs(transform.position.x) + Managers.Game.camera_h)
        {
            transform.position = Managers.Game.player.gameObject.transform.position + z;
        }
        else if(Mathf.Abs(Managers.Game.player.gameObject.transform.position.y) > Mathf.Abs(transform.position.y) + Managers.Game.camera_v)
        {
            transform.position = Managers.Game.player.gameObject.transform.position + z;
        }
    }
}
