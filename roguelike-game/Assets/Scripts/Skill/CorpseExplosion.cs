using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class CorpseExplosion : Base_Skill
{
    public Animator anime;
    protected override void Start()
    {
        base.Start();
        anime.Play("boom");
    }
    protected override void Update()
    {
        if(anime.GetCurrentAnimatorStateInfo(0).IsName("boom"))
        {
            if(anime.GetCurrentAnimatorStateInfo(0).normalizedTime == 1f) { Managers.Game.objectPool.disableObject(this.GetType().Name, this.gameObject); }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Monster") { collision.gameObject.GetComponent<Monster_Controller>().Hp -= skill.skillDamage; }
    }
}
