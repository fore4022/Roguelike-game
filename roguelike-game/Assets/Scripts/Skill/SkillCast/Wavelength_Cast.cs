using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Wavelength_Cast : Base_SkillCast
{
    protected override IEnumerator skillCast()
    {
        while(true)
        {
            go = Managers.Game.objectPool.activateObject(typeof(Base_SkillCast), prefabName);
            Wavelength newScript = go.AddComponent<Wavelength>();
            newScript.skill = skill;
            newScript.anime = go.AddComponent<Animator>();
            newScript.anime.runtimeAnimatorController = animeController;
            BoxCollider2D boxCollider = go.AddComponent<BoxCollider2D>();
            boxCollider.isTrigger = true;
            yield return new WaitForSeconds(skill.skillCoolTime);
        }
    }
}
