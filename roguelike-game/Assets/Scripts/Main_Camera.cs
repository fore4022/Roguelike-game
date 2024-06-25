using System;
using UnityEngine;
[Obsolete]
public class Main_Camera : MonoBehaviour
{
    private Camera _cam;
    private Vector3 _vec;

    [SerializeField]
    private float value = 0.5f;

    private void Start()
    {
        _cam = GetComponent<Camera>();

        _cam.orthographicSize = 8;

        this.gameObject.transform.position = Managers.Game.player.gameObject.transform.position;

        Managers.Input.keyAction -= ZoomOut;
        Managers.Input.keyAction += ZoomOut;
    }
    private void LateUpdate()
    {
        if (!Managers.Game.player) { return; }

        _vec = Vector3.Lerp(transform.position, Managers.Game.player.transform.position, Time.deltaTime);
        transform.position = new Vector3(_vec.x, _vec.y, -1);

        if(Managers.Game.player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle") && _cam.orthographicSize > 8)
        {
            if (_cam.orthographicSize > 8) { _cam.orthographicSize -= 1 * Time.deltaTime * value; }
            else { _cam.orthographicSize = 8; }
        }
    }
    private void ZoomOut()
    {
        if (_cam.orthographicSize < 13) { _cam.orthographicSize += 1 * Time.deltaTime * value; }
        else { _cam.orthographicSize = 13; }
    }
}
