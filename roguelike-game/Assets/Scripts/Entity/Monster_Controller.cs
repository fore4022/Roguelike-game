using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
public class Monster_Controller : Base_Controller
{
    [HideInInspector]
    public Monster monsterType;

    [HideInInspector]
    public float slowDownAmount;

    private enum State
    {
        Moving,
        Death
    }

    private State _state;

    protected float interval = 0.2f;
    protected override void Start()
    {
        base.Start();

        _state = State.Moving;

        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;

        anime = GetComponent<Animator>();
        anime.speed = 0.25f;
    }
    private void OnEnable() { Init(); }
    protected override void Init()
    {
        damage = monsterType.attackDamage;
        maxHp = monsterType.maxHp;
        gold = monsterType.gold;
        exp = monsterType.exp;

        moveSpeed = monsterType.moveSpeed;
        hp = maxHp;
    }
    protected override void Update()
    {
        if (Managers.Game.player.Hp > 0)
        {
            if (_state == State.Death) { return; }

            if (Hp <= 0)
            {
                boxCollider.enabled = false;
                _state = State.Death;
            }

            SetState();
        }
    }
    private void SetState()
    {
        switch (_state)
        {
            case State.Moving:
                Moving();
                break;
            case State.Death:
                StartCoroutine(Death());
                break;
        }
    }
    private Vector3 Separation(IEnumerable<Monster_Controller> monsters)
    {
        Vector3 vec = Vector3.zero;

        foreach (Monster_Controller boid in monsters) { vec += (transform.position - boid.transform.position).normalized; }

        vec /= monsters.Count();
        vec *= MoveSpeed * 2 * Time.deltaTime;

        if (Mathf.Abs(vec.x) < 0.000075f || Mathf.Abs(vec.y) < 0.000075f) { vec = Vector3.zero; }

        return vec;
    }
    private Vector3 Move() { return (Managers.Game.player.gameObject.transform.position - transform.position).normalized; }
    protected override void Moving()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, (transform.localScale.x + transform.localScale.y) / 2.75f, LayerMask.GetMask("Monster"));

        List<Player_Controller> players = colliders.Select(o => o.gameObject.GetComponent<Player_Controller>()).ToList();
        List<Monster_Controller> monsters = colliders.Select(o => o.gameObject.GetComponent<Monster_Controller>()).ToList();

        players.RemoveAll(o => o == null);
        monsters.RemoveAll(o => o == null);

        transform.position += Move() * MoveSpeed * Time.deltaTime + Separation(monsters) * slowDownAmount / 100f;

        if (players.Count() == 1 && monsters.Count() != 1) { transform.position += Separation(monsters) * slowDownAmount / 100f; }
    }
    public virtual void Attacked(int damage) { hp -= damage; }
    protected override IEnumerator Death() 
    {
        anime.speed = 1f;
        anime.Play("death");

        Managers.Game.player.GetLoot(gold, exp);

        while (true)
        {
            if(anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f && anime.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                Managers.Game.objectPool.DisableObject(monsterType.monsterName, this.gameObject);
                break;
            }
            yield return null;
        }
    }
    public int MonsterCount()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, (transform.localScale.x + transform.localScale.y) / 2, LayerMask.GetMask("Monster"));

        return colliders.Count();
    }
    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, (transform.localScale.x + transform.localScale.y) / 2.75f);
    }
}