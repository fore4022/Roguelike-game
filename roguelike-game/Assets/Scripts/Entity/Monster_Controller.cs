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
    protected float interval = 0.2f;
    protected GameObject player;
    protected override void Start()
    {
        init();
        base.Start();
        state = State.Moving;
    }
    protected void OnEnable()
    {
        player = Managers.Game.playerController.gameObject;
    }
    protected override void init()
    {
        attackDamage = monsterType.attackDamage;
        attackSpeed = monsterType.attackSpeed;
        moveSpeed += monsterType.moveSpeed;
        hp = monsterType.hp;
        gold = monsterType.gold;
        exp = monsterType.exp;
    }
    protected override void Update()
    {
        base.Update();
    }
    protected Vector3 separation(IEnumerable<Monster_Controller> monsters)
    {
        Vector3 vec = Vector3.zero;
        foreach (Monster_Controller boid in monsters) { vec += (transform.position - boid.transform.position).normalized; }
        vec /= monsters.Count();
        vec *= MoveSpeed * 2 * Time.deltaTime;
        if (Mathf.Abs(vec.x) < 0.00075f || Mathf.Abs(vec.y) < 0.00075f) { vec = Vector3.zero; }
        return vec;
    }
    protected Vector3 move() { return (player.transform.position - transform.position).normalized; }
    protected override void moving()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, (transform.localScale.x + transform.localScale.y) / 2.75f);
        List<Player_Controller> players = colliders.Select(o => o.gameObject.GetComponent<Player_Controller>()).ToList();
        List<Monster_Controller> monsters = colliders.Select(o => o.gameObject.GetComponent<Monster_Controller>()).ToList();
        players.RemoveAll(o => o == null);
        monsters.RemoveAll(o => o == null);
        if (players.Count() != 1 && monsters.Count() == 1) { transform.position += move() * MoveSpeed * Time.deltaTime; }
        else { transform.position += separation(monsters); }
    }
    protected override void death()
    {
        Managers.Game.playerController.Gold += gold;
        Managers.Game.playerController.Exp += exp;
    }
    protected void crash(Collision2D collision)
    {//플레이어 체력 감소 메서드 실행으로 바꾸기
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) { Managers.Game.playerController.Hp -= attackDamage * attackSpeed * Time.deltaTime; }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        crash(collision);
    }
    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        crash(collision);
    }
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, (transform.localScale.x + transform.localScale.y) / 2.75f);
    }
}