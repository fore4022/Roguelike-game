using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BloodyMassacre_Cast : Base_SkillCast
{
    public override IEnumerator skillCast()
    {
        while(true)
        {
            GameObject go = Managers.Game.objectPool.activateObject(typeof(Base_SkillCast), prefabName);
            Base_Skill baseSkill = go.AddComponent(script) as Base_Skill;
            baseSkill.skill = skill;
            baseSkill.anime = go.AddComponent<Animator>();
            BoxCollider2D boxCollider = go.AddComponent<BoxCollider2D>();
            boxCollider.isTrigger = true;
            yield return new WaitForSeconds(skill.skillCoolTime);
        }
    }
}
