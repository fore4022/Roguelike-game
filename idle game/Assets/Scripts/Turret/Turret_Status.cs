using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Turret_Status : MonoBehaviour
{
    private float damage;
    private float attackSpeed;
    private float criticalChance;
    private float criticalDamage;
    private float aimSpeed;
    public float _damage { get { return damage; } set { damage = value; } }
    public float _attackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
    public float _criticalChance { get { return criticalChance; } set { criticalChance = value; } }
    public float _criticalDamage { get { return criticalDamage; } set { criticalDamage = value; } }
    public float _aimSpeed { get { return aimSpeed; } set { aimSpeed = value; } }
}
