using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Define
{
    public enum UIEvent
    {
        Click,
        Enter,
        Exit,
        Down,
        Up,
        BeginDrag,
        Drag,
        EndDrag
    }
    public enum Effect
    {
        Null,
        SlowDown = 40,
        UnableToMove = 100
    }
}
