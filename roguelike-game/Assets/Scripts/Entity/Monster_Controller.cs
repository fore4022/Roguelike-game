using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Monster_Controller : Base_Controller
{
    protected override void Start()
    {
        base.Start();
        state = State.Moving;
    }
    protected override void moving()
    {
        transform.position += (Managers.Game.PlayerController.gameObject.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
    }
    protected override void death()
    {
        Managers.Game.PlayerController.Gold += gold;
        Managers.Game.PlayerController.Exp += exp;
    }
    protected void crash(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Managers.Game.PlayerController.Hp -= damage * attackSpeed * Time.deltaTime;
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        crash(collision);
    }
    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        crash(collision);
    }
}
