using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Equipment", menuName = "Create New Item/New Equipment")]
public class Equipment : Item
{
    public int hp;
    public int damage;
    public int skillCooldownReduction;

    public float moveSpeed;
    public float expMagnification;
    public float goldMagnification;

}
