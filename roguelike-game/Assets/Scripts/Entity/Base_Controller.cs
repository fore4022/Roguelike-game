using System;
using UnityEngine;
[Obsolete]
public abstract class Base_Controller : MonoBehaviour, IAttackable, IDamageable, IDieable, IMovable, IStatus
{
    protected Stat stat;
    protected Animator anime;
    protected BoxCollider2D boxCollider;
    protected Rigidbody2D rigid;

    public float MoveSpeed { get => stat.moveSpeed; protected set => stat.moveSpeed += value; }
    public float Range { get => stat.range; protected set => stat.range += value; }
    public int Damage { get => stat.damage; protected set => stat.damage += value; }
    public int MaxHp { get => stat.maxHp; protected set => stat.maxHp += value; }
    public int Hp { get => stat.hp; protected set => stat.hp += value; }

    public abstract void Attack();
    public abstract void Die();
    public abstract void Move();
    public abstract void TakeDamage();
    protected void Start()
    {
        anime = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();

        anime.speed = stat.animationPlaySpeed;

        rigid.gravityScale = 0f;
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
