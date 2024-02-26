using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Android.Types;
using UnityEngine;
public class BloodyMassacre : Base_Skill
{
    public Animator anime;
    private float continueSkill;
    protected override void init()
    {
        continueSkill = 0f;
        transform.localScale = new Vector3(skill.skillRange * 2, skill.skillRange * 2, 1);
    }
    protected override void Update()
    {
        continueSkill += Time.deltaTime;
        transform.position = Managers.Game.player.gameObject.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, skill.skillRange / 2, LayerMask.GetMask("Monster"));
        foreach (Collider2D col in colliders)
        {
            Monster_Controller monster = col.gameObject.GetComponent<Monster_Controller>();
            if (skill.effect == Define.Effect.SlowDown) { monster.slowDownAmount = (int)Define.Effect.SlowDown; }
            else if (skill.effect == Define.Effect.UnableToMove) { monster.slowDownAmount = (int)Define.Effect.UnableToMove; }
            monster.attacked(skill.skillDamage);
        }
        if(continueSkill >= skill.skillDuration) { Managers.Game.objectPool.disableObject(this.GetType().Name, this.gameObject); }
    }
}
