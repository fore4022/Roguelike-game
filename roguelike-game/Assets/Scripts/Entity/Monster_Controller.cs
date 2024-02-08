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
    protected GameObject player;
    protected enum State
    {
        Moving, Death
    }
    protected State state;
    protected float interval = 0.2f;
    protected override void Start()
    {
        base.Start();
        state = State.Moving;
    }
    protected void OnEnable()
    {
        player = Managers.Game.player.gameObject;
        init();
    }
    protected override void init()
    {
        attackDamage = monsterType.attackDamage + Managers.Game.minute / 2;
        moveSpeed = monsterType.moveSpeed;
        hp = monsterType.hp + Managers.Game.minute / 2;
        gold = monsterType.gold + (Managers.Game.minute / 4f);
        exp = monsterType.exp + (Managers.Game.minute / 4f);
    }
    protected override void Update()
    {
        if (state == State.Death) { return; }
        if (Hp == 0) { anime.Play("death"); state = State.Death; }
        setAnime();
        setState();
    }
    protected override void setAnime()
    {
        
    }
    private void setState()
    {
        switch (state)
        {
            case State.Moving:
                moving();
                break;
            case State.Death:
                death();
                break;
        }
    }
    protected Vector3 separation(IEnumerable<Monster_Controller> monsters)
    {
        Vector3 vec = Vector3.zero;
        foreach (Monster_Controller boid in monsters) { vec += (transform.position - boid.transform.position).normalized; }
        vec /= monsters.Count();
        vec *= MoveSpeed * 2 * Time.deltaTime;
        if (Mathf.Abs(vec.x) < 0.000075f || Mathf.Abs(vec.y) < 0.000075f) { vec = Vector3.zero; }
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
    protected override void death() { Managers.Game.player.getLoot(gold, exp); }
    protected void crash(Collision2D collision) { Managers.Game.player.attacked(attackDamage); }
    protected virtual void OnCollisionEnter2D(Collision2D collision) { crash(collision); }
    protected virtual void OnCollisionStay2D(Collision2D collision) { crash(collision); }
    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, (transform.localScale.x + transform.localScale.y) / 2.75f);
    }
}