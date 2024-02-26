using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class BloodMagicBullet_Cast : Base_SkillCast
{
    protected override IEnumerator skillCast()
    {
        while (true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Managers.Game.player.gameObject.transform.position, Camera.main.orthographicSize * 2 + 1.5f, LayerMask.GetMask("Monster"));
            if(colliders == null) { yield return null; }
            go = Managers.Game.objectPool.activateObject(typeof(Base_SkillCast), prefabName);
            BloodMagicBullet newScript = gameObject.AddComponent(script) as BloodMagicBullet;
            newScript.skill = skill;
            newScript.anime = go.AddComponent<Animator>();
            newScript.anime.runtimeAnimatorController = animeController;
            BoxCollider2D boxCollider = go.AddComponent<BoxCollider2D>();
            boxCollider.isTrigger = true;
            yield return new WaitForSeconds(skill.skillCoolTime);
        }
    }
}
