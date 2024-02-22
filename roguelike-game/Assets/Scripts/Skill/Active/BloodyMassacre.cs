using System.Collections;
using System.Collections.Generic;
using Unity.Android.Types;
using UnityEngine;
public class BloodyMassacre : Base_Skill
{
    private float continueSkill;
    protected override void Start()
    {
        continueSkill = 0f;
    }
    protected override void Update()
    {
        continueSkill += Time.deltaTime;
        transform.position = Managers.Game.player.gameObject.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, skill.skillRange / 2, LayerMask.NameToLayer("Monster"));
        foreach(Collider2D col in colliders)
        {
            Monster_Controller monster = col.gameObject.GetComponent<Monster_Controller>();
            if(skill.effect == Define.Effect.SlowDown) { monster.slowDownAmount = (int)Define.Effect.SlowDown; }
            else if(skill.effect == Define.Effect.UnableToMove) { monster.slowDownAmount = (int)Define.Effect.UnableToMove; }
            monster.attacked(skill.skillDamage);
        }
        if(continueSkill == skill.skillDuration) { Destroy(this.gameObject); }
    }
}
