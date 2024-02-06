using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Status : MonoBehaviour
{
    [SerializeField]
    protected float attackDamage;
    [SerializeField]
    protected float attackSpeed;
    [SerializeField]
    protected float hp;
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected float gold;
    [SerializeField]
    protected float exp;
    public float AttackDamage { get { return attackDamage; } set { attackDamage = value; } }
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
    public float Hp { get { return hp; } set { hp = value; } }
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float Gold { get { return gold; } set { gold = value; } }
    public float Exp { get { return exp; } set { exp = value; } }
}