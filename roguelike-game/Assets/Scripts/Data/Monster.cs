using System;
using UnityEngine;
[Obsolete]
[CreateAssetMenu(fileName = "Monster", menuName = "Create New Monster/New Monster")]
public class Monster : ScriptableObject
{
    public enum MonsterType
    {
        CloseDistance, LongDistance
    }
    public Stat stat;

    public MonsterType monsterType;

    public string monsterName;
    public float moveSpeed;
    public int gold;
    public int exp;
}
