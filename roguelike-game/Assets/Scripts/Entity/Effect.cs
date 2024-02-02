using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Effect : MonoBehaviour
{
    private float amountOfSlowdown;
    private float duration;
    public static void effectActivated(Define.Effect effect, List<Monster_Controller> monsters)
    {
        switch(effect)
        {
            case Define.Effect.SlowDown:
                slowDown(monsters);
                break;
            case Define.Effect.UnableToMove:
                unableToMove(monsters);
                break;
        }
    }
    private static void slowDown(List<Monster_Controller> monsters)//amountOfSlowdown, duration
    {

    }
    private static void unableToMove(List<Monster_Controller> monsters)//duration
    {

    }
}
