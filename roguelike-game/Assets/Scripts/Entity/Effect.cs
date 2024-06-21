using System.Collections.Generic;
using UnityEngine;
public class Effect : MonoBehaviour
{
    private float _amountOfSlowdown;
    private float _duration;
    public static void EffectActivated(Define.Effect effect, List<Monster_Controller> monsters)
    {
        switch(effect)
        {
            case Define.Effect.SlowDown:
                SlowDown(monsters);
                break;
            case Define.Effect.UnableToMove:
                UnableToMove(monsters);
                break;
        }
    }
    private static void SlowDown(List<Monster_Controller> monsters)//amountOfSlowdown, duration
    {

    }
    private static void UnableToMove(List<Monster_Controller> monsters)//duration
    {

    }
}
