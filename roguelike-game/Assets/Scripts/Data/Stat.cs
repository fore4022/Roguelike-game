using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stat : MonoBehaviour
{
    [SerializeField]
    protected float attackDamage;
    [SerializeField]
    protected float hp;
    [SerializeField]
    protected float maxHp;
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected float gold;
    [SerializeField]
    protected float exp;
    public float AttackDamage { get { return attackDamage; } set { attackDamage = value; } }
    public float Hp { get { return hp; } set { hp = value; } }
    public float MaxHp { get { return maxHp; } set { maxHp = value; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float Gold { get { return gold; } set { gold = value; } }
    public float Exp { get { return exp; } set { exp = value; } }
}