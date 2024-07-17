using UnityEngine;
using System;
public class Input_Manager
{
    public Action keyAction = null;
    public void OnUpdate()
    {
#if UNITY_EDITOR
        {
            if (Input.anyKey == false) { return; }
        }
#endif

        if (keyAction != null) { keyAction.Invoke(); }
    }
}
