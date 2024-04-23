using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Monster", menuName = "Create New Monster/New Monster")]
public class Monster : ScriptableObject
{
    public enum MonsterType
    {
        closeDistance, longDistance
    }
    public MonsterType monsterType;

    public string monsterName;

    public float moveSpeed;

    public int attackDamage;
    public int hp;
    public int maxHp;
    public int attackSpeed;
    public int gold;
    public int exp;
}
