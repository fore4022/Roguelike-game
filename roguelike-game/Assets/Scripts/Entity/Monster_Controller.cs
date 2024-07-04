using System;
using System.Collections;
using UnityEngine;
[Obsolete]
public class Monster_Controller : Base_Controller
{
    public Monster monster;

    public override void Attack()
    {

    }
    public override void TakeDamage()
    {

    }
    public override void Die()
    {

    }
    public override void Move()
    {

    }
    private void OnEnable()
    {
        StartCoroutine(Moving());
    }
    private Vector3 Direction() { return (Managers.Game.player.gameObject.transform.position - transform.position).normalized; }
    public virtual void Attacked(int damage)
    {
        Hp -= damage;

        if(Hp <= 0)
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

        //Managers.Game.player.GetLoot(gold, exp);

        while (anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) { yield return null; }

        Managers.Game.objectPool.DisableObject(monster.monsterName, gameObject);
    }
    public int MonsterCount()
    {
        Collider2D[] colliders = new Collider2D[] { };

        return Physics2D.OverlapCircleNonAlloc(transform.position, Range, colliders, LayerMask.GetMask("Monster"));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}