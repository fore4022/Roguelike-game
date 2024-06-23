using System.Collections;
using UnityEngine;
public class Monster_Controller : Base_Controller
{
    [SerializeField]
    private float animeSpeed = 0.25f;

    public Monster monsterType;

    private float _range = 2f;
    protected override void Start()
    {
        base.Start();

        anime.speed = animeSpeed;
    }
    private void OnEnable()
    { 
        Init();

        StartCoroutine(Moving());
    }
    protected override void Init()
    {
        damage = monsterType.attackDamage;
        maxHp = monsterType.maxHp;
        gold = monsterType.gold;
        exp = monsterType.exp;

        moveSpeed = monsterType.moveSpeed;
        hp = maxHp;
    }
    private Vector3 Direction() { return (Managers.Game.player.gameObject.transform.position - transform.position).normalized; }
    public virtual void Attacked(int damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            StopCoroutine(Moving());

            StartCoroutine(Death());
        }
    }
    private IEnumerator Moving() 
    {
        while (true)
        {
            transform.position += Direction() * MoveSpeed * Time.deltaTime;

            yield return null;
        }  
    }
    private IEnumerator Death() 
    {
        anime.speed++;
        anime.Play("death");

        Managers.Game.player.GetLoot(gold, exp);

        while (anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) { yield return null; }

        Managers.Game.objectPool.DisableObject(monsterType.monsterName, gameObject);
    }
    public int MonsterCount()
    {
        Collider2D[] colliders = new Collider2D[] { };

        return Physics2D.OverlapCircleNonAlloc(transform.position, _range, colliders, LayerMask.GetMask("Monster"));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}