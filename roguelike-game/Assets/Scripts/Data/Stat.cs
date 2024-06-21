using UnityEngine;
public class Stat : MonoBehaviour
{
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected int maxHp;
    [SerializeField]
    protected int hp;
    [SerializeField]
    protected int gold;
    [SerializeField]
    protected int exp;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public int Damage { get { return damage; } set { damage = value; } }
    public int MaxHp { get { return maxHp; } set { maxHp = value; } }
    public int Hp { get { return hp; } set { hp = value; } }
    public int Gold { get { return gold; } set { gold = value; } }
    public int Exp { get { return exp; } set { exp = value; } }
}