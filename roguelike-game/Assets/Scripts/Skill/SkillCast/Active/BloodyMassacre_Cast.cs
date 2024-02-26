using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
            BloodyMassacre baseSkill =  go.AddComponent(script) as BloodyMassacre;
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
