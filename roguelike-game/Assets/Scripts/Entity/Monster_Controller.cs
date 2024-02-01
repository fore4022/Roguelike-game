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
    protected float interval = 0.5f;
    protected Vector3 separationVec;
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
    protected void separation(IEnumerable<Monster_Controller> monsters)
    {
        separationVec = Vector3.zero;
        if(monsters.Count() != 1)
        {
            foreach(Monster_Controller boid in monsters)
            {
                separationVec += (transform.position - boid.transform.position).normalized;
            }
            separationVec /= monsters.Count();
        }
    }
    protected Vector3 move() { return (player.transform.position - transform.position).normalized; }
    protected override void moving()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector3(transform.localScale.x + interval, transform.localScale.y + interval, 0), default);
        List<Player_Controller> players = colliders.Select(o => o.gameObject.GetComponent<Player_Controller>()).ToList();
        List<Monster_Controller> monsters = colliders.Select(o => o.gameObject.GetComponent<Monster_Controller>()).ToList();
        players.RemoveAll(o => o == null);
        monsters.RemoveAll(o => o == null);
        if (players.Count() != 1 && monsters.Count() == 1) { transform.position += move() * MoveSpeed * Time.deltaTime; }
        else
        {
            //if(Mathf.Abs(separationVec.x) * moveSpeed * 2 > 0.1f || Mathf.Abs(separationVec.y) * moveSpeed * 2 > 0.1f)
            //{
            //}
                transform.position += separationVec * MoveSpeed * 2 * Time.deltaTime;
        }
    }
    protected override void death()
    {
        Managers.Game.PlayerController.Gold += gold;
        Managers.Game.PlayerController.Exp += exp;
    }
    protected void crash(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) { Managers.Game.PlayerController.Hp -= damage * attackSpeed * Time.deltaTime; }
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
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.localScale.x + interval, transform.localScale.y + interval, 0));
    }
}