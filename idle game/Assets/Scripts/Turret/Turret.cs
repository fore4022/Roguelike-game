using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Turret : Turret_Status
{
    protected enum State
    {
        Idle, Attack, CriticalAttack, Rotate
    }
    protected State state;
    protected virtual void setState()
    {
        switch(state)
        {
            case State.Idle:
                idle();
                break;
            case State.Attack:
                attack();
                break;
            case State.CriticalAttack:
                criticalAttack();
                break;
            case State.Rotate:
                rotate();
                break;
        }
    }
    protected virtual void idle() { }
    protected virtual void attack() { }
    protected virtual void criticalAttack() { }
    protected virtual void rotate() { }
}
