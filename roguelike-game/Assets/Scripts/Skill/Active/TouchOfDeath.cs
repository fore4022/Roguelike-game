using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
public class TouchOfDeath : Base_Skill
{
    public float delayTime;
    protected override void init() { transform.rotation = new Quaternion(0, 0, Random.Range(0, 361), 0); }
    protected override void Update()
    {
        delayTime -= Time.deltaTime;
        if(delayTime <= 0) { anime.Play("");/*animation name*/ }
        if (anime.GetCurrentAnimatorStateInfo(0).IsName(""))//animation name
        {
            if(anime.GetCurrentAnimatorStateInfo(0).normalizedTime == 1f) { Managers.Game.objectPool.disableObject(this.GetType().Name, this.gameObject); }
        }
    }
    private void attack(Collider2D collision) { collision.gameObject.GetComponent<Monster_Controller>().attacked(skill.skillDamage); }
    private void OnTriggerEnter2D(Collider2D collision) { if (collision.gameObject.CompareTag("Monster")) { attack(collision); } }
    private void OnTriggerStay2D(Collider2D collision) { if (collision.gameObject.CompareTag("Monster")) { attack(collision); } }
}
