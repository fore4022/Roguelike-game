using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ForceField_Cast : Base_SkillCast
{
    protected override IEnumerator skillCast()
    {
        while (true)
        {   
            go = Managers.Game.objectPool.activateObject(typeof(Base_Skill), prefabName);
            baseSkill = go.GetComponent(script) as Base_Skill;
            baseSkill.skill = skill;
            yield return null;
        }
    }
}
