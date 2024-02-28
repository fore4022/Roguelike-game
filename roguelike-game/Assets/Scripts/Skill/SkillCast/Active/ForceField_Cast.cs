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
            go = Managers.Game.objectPool.activateObject(typeof(Base_SkillCast), prefabName);
            ForceField newScript = go.AddComponent(script) as ForceField;
            newScript.skill = skill;
            yield return new WaitForSeconds(skill.skillCoolTime);
        }
    }
}
