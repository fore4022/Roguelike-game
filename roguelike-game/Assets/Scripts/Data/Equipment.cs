using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
[CreateAssetMenu(fileName = "Equipment", menuName = "Create New Item/New Equipment")]
public class Equipment : Item
{
    public int range;
    public int attackDelay;
    public int attackDamage;
    public int collDown;
    public int speed;
    public int health;

    public bool penetrate;

    public object this [int index]
    {
        get
        {
            if(index == 0) { return (range == 0) ? null : range; }
            else if(index == 1) { return (attackDelay == 0) ? null : attackDelay; }
            else if(index == 2) { return (attackDamage == 0) ? null : attackDamage; }
            else if(index == 3) { return (collDown == 0) ? null : collDown; }
            else if(index == 4) { return (speed == 0) ? null : speed; }
            else if(index == 5) { return (health == 0) ? null : health; }
            else if(index == 6) { return penetrate; }
            else { return null; }
        }
    }
}
