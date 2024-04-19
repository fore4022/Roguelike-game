using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
public class Wavelength : Base_Skill
{
    public Animator anime;
    //private int projectileSpeed;
    protected override void init()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        //anime.play(?)
        //projectileSpeed = 12;
    }
    protected override void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Monster"))
        {
            collision.GetComponent<Monster_Controller>().attacked(skill.skillDamage);
        }
    }
    private void OnBecameInvisible() { Managers.Game.objectPool.disableObject(this.GetType().Name, this.gameObject); }
}
