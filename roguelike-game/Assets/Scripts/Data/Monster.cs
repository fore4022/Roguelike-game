using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Monster", menuName = "Create New Monster/New Monster")]
public class Monster : ScriptableObject
{
    public string monsterName;
    public float attackDamage;
    public float attackSpeed;
    public float moveSpeed;
    public int hp;
    public int gold;
    public int exp;
}
