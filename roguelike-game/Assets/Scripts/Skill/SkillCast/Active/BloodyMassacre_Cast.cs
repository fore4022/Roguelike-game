using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BloodyMassacre_Cast : Base_SkillCast
{
    private SpriteRenderer render;
    private int colorA;
    public override IEnumerator skillCast()
    {
        colorA = 45;
        while(true)
        {
            GameObject go = Managers.Game.objectPool.activateObject(typeof(Base_SkillCast), prefabName);
            System.Type type = System.Type.GetType(prefabName);
            Base_Skill baseSkill = go.AddComponent(type) as Base_Skill;
            baseSkill.skill = skill;
            baseSkill.anime = go.AddComponent<Animator>();
            baseSkill.anime.runtimeAnimatorController = animeController;
            render = go.GetComponent<SpriteRenderer>();
            render.material.color = new Color(render.material.color.r, render.material.color.g, render.material.color.b, colorA);
            yield return new WaitForSeconds(skill.skillCoolTime + skill.skillDuration);
        }
    }
}
