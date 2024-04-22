using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
public class Main_Camera : MonoBehaviour
{
    private Camera cam;
    private Vector3 vec;

    [SerializeField]
    private float value = 0.5f;

    private void Start()
    {
        cam = GetComponent<Camera>();
        Managers.Input.keyAction -= zoomOut;
        Managers.Input.keyAction += zoomOut;
    }
    private void LateUpdate()
    {
        if (!Managers.Game.player) { return; }

        vec = Vector3.Lerp(transform.position, Managers.Game.player.transform.position, Time.deltaTime);
        transform.position = new Vector3(vec.x, vec.y, -1);

        if(Managers.Game.player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle") && cam.orthographicSize > 8)
        {
            if (cam.orthographicSize > 8) { cam.orthographicSize -= 1 * Time.deltaTime * value; }
            else { cam.orthographicSize = 8; }
        }
    }
    private void zoomOut()
    {
        if (cam.orthographicSize < 10) { cam.orthographicSize += 1 * Time.deltaTime * value; }
        else { cam.orthographicSize = 10; }
    }
}
