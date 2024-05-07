using System.Collections;
using System.Collections.Generic;
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
            if (keyAction != null) { keyAction.Invoke(); }
        }
#endif
#if UNITY_ANDROID
        {
            if(keyAction != null) { keyAction.Invoke(); }
        }
#endif
    }
}
