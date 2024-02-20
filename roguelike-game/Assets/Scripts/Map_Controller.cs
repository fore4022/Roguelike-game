using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Map_Controller : MonoBehaviour
{
    private void Start()
    {
        transform.localScale = new Vector2(Camera.main.orthographicSize * 2 * Camera.main.aspect, Camera.main.orthographicSize * 2) * 2;
    }
    private void Update()
    {
        if(Managers.Game.player.gameObject.transform.position.x + Camera.main.orthographicSize < transform.position.x + transform.localScale.x)
        {
            
        }
    }
}
