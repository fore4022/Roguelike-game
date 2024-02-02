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
    protected float interval = 0.2f;
    protected override void Start()
    {
        base.Start();
        state = State.Moving;
        moveSpeed = 1;
        hp = 1;
    }
    protected override void Update()
    {
        base.Update();
    }
    protected Vector3 separation(IEnumerable<Monster_Controller> monsters)
    {
        Vector3 vec = Vector3.zero;
        if(monsters.Count() != 1)
        {
            foreach (Monster_Controller boid in monsters) { vec += (transform.position - boid.transform.position).normalized; }
            vec /= monsters.Count();
            if(Mathf.Abs(vec.x) < 0.075f || Mathf.Abs(vec.y) < 0.075f) { vec = Vector3.zero; }
        }
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
        else { transform.position += separation(monsters) * MoveSpeed * 2 * Time.deltaTime; }
    }
    protected override void death()
    {
        Managers.Game.PlayerController.Gold += gold;
        Managers.Game.PlayerController.Exp += exp;
    }
    protected void crash(Collision2D collision)
    {//플레이어 체력 감소 메서드 실행으로 바꾸기
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) { Debug.Log("Asdf"); Managers.Game.PlayerController.Hp -= attackDamage * attackSpeed * Time.deltaTime; }
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