using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BloodyMassacre_Cast : Base_SkillCast
{
    private SpriteRenderer render;
    private float colorA;
    protected override IEnumerator skillCast()
    {
        colorA = 0.15f;
        while(true)
        {
            go = Managers.Game.objectPool.activateObject(typeof(Base_SkillCast), prefabName);
            System.Type type = System.Type.GetType(prefabName);
            baseSkill = go.AddComponent(type) as Base_Skill;
            baseSkill.skill = skill;
            baseSkill.anime = go.AddComponent<Animator>();
            baseSkill.anime.runtimeAnimatorController = animeController;
            //anime.play?
            render = go.GetComponent<SpriteRenderer>();
            render.color = new Color(render.color.r, render.color.g, render.color.b, colorA);
            yield return new WaitForSeconds(skill.skillCoolTime + skill.skillDuration);
        }
    }
}
