using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Search;
using UnityEngine;
[CreateAssetMenu(fileName = "Expendables", menuName = "Create New Item/New Expendables")]
public class Expendables : Item
{
    public int attackDamage;
    public int collDown;
    public int health;
    public int speed;
    public int eyesight;

    public int goldMagnification;
    public int level;
    public int healthRegenerationPerSecond;
    public int ExpMagnification;

    public int this [int index]
    {
        get
        {
            if(index == 0) { return attackDamage; }
            else if(index == 1) { return collDown; }
            else if(index == 2) { return health; }
            else if(index == 3) { return speed; }
            else if(index == 4) { return eyesight; }
            else if(index == 5) { return goldMagnification; }
            else if(index == 6) { return level; }
            else if(index == 7) { return healthRegenerationPerSecond; }
            else if(index == 8) { return ExpMagnification; }
            else { return 0; }
        }
    }
}
