using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
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
    private State state;

    protected float interval = 0.2f;
    protected override void Start()
    {
        base.Start();
        state = State.Moving;
    }
    private void OnEnable() { init(); }
    protected override void init()
    {
        attackDamage = monsterType.attackDamage + (int)Managers.Game.minute / 2;//
        maxHp = monsterType.maxHp + (int)Managers.Game.minute / 2;
        gold = monsterType.gold + (int)(Managers.Game.minute / 4f);
        exp = monsterType.exp + (int)(Managers.Game.minute / 4f);

        moveSpeed = monsterType.moveSpeed;
        hp = maxHp;
    }
    protected override void Update()
    {
        if(Managers.Game.player.Hp > 0)
        {
            if (state == State.Death) { return; }
            if (Hp <= 0)
            {
                boxCollider.enabled = false;
                state = State.Death;
            }
            setState();
        }
    }
    private void setState()
    {
        switch (state)
        {
            case State.Moving:
                moving();
                break;
            case State.Death:
                StartCoroutine(death());
                break;
        }
    }
    private Vector3 separation(IEnumerable<Monster_Controller> monsters)
    {
        Vector3 vec = Vector3.zero;
        foreach (Monster_Controller boid in monsters) { vec += (transform.position - boid.transform.position).normalized; }
        vec /= monsters.Count();
        vec *= MoveSpeed * 2 * Time.deltaTime;
        if (Mathf.Abs(vec.x) < 0.000075f || Mathf.Abs(vec.y) < 0.000075f) { vec = Vector3.zero; }
        return vec;
    }
    private Vector3 move() { return (Managers.Game.player.gameObject.transform.position - transform.position).normalized; }
    protected override void moving()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, (transform.localScale.x + transform.localScale.y) / 2.75f, LayerMask.GetMask("Monster"));

        List<Player_Controller> players = colliders.Select(o => o.gameObject.GetComponent<Player_Controller>()).ToList();
        List<Monster_Controller> monsters = colliders.Select(o => o.gameObject.GetComponent<Monster_Controller>()).ToList();
        players.RemoveAll(o => o == null);
        monsters.RemoveAll(o => o == null);

        transform.position += move() * MoveSpeed * Time.deltaTime + separation(monsters) * slowDownAmount / 100f;
        if (players.Count() == 1 && monsters.Count() != 1) { transform.position += separation(monsters) * slowDownAmount / 100f; }
    }
    public virtual void attacked(int damage) { hp -= damage; }
    protected override IEnumerator death() 
    {
        anime.Play("death");
        while (true)
        {
            if(anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f && anime.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                Managers.Game.player.getLoot(gold, exp);
                Managers.Game.objectPool.disableObject(monsterType.monsterName, this.gameObject);
                break;
            }
            yield return null;
        }
    }
    public int monsterCount()
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