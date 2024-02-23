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
            System.Type type = System.Type.GetType(prefabName);
            Base_Skill baseSkill = go.AddComponent(type) as Base_Skill;
            baseSkill.skill = skill;
            baseSkill.anime = go.AddComponent<Animator>();
            yield return new WaitForSeconds(skill.skillCoolTime + skill.skillDuration);
        }
    }
}
