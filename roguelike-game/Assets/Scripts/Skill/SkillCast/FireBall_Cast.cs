using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class FireBall_Cast : Base_SkillCast
{
    protected override IEnumerator SkillCast()
    {
        while (true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Managers.Game.player.gameObject.transform.position, Managers.Game.camera_h + 1.5f, LayerMask.GetMask("Monster"));
            List<Monster_Controller> monsters = colliders.Select(o => o.gameObject.GetComponent<Monster_Controller>()).ToList();
            
            if (monsters.Count() == 0) { yield return null; }

            go = Managers.Game.objectPool.ActivateObject(typeof(Base_SkillCast), skillName);

            FireBall newScript = go.AddComponent(script) as FireBall;

            newScript.skill = skill;
            newScript.anime = go.AddComponent<Animator>();
            newScript.anime.runtimeAnimatorController = animeController;

            BoxCollider2D boxCollider = go.AddComponent<BoxCollider2D>();

            boxCollider.isTrigger = true;

            yield return new WaitForSeconds(skill.skillCoolTime);
        }
    }
}
