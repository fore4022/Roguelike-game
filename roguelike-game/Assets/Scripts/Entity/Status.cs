using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Status : MonoBehaviour
{
    [SerializeField]
    protected float damage;
    [SerializeField]
    protected float attackSpeed;
    [SerializeField]
    protected float hp;
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected int gold;
    [SerializeField]
    protected int exp;
    public float Damage { get { return damage; } set { damage = value; } }
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
    public float Hp { get { return hp; } set { hp = value; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public int Gold { get { return gold; } set { gold = value; } }
    public int Exp { get { return exp; } set { exp = value; } }
}