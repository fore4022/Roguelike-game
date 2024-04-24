using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class BloodyMassacre_Cast : Base_SkillCast
{
    protected override IEnumerator skillCast()
    {
        while(true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Managers.Game.player.gameObject.transform.position, Managers.Game.camera_h + 1.5f, LayerMask.GetMask("Monster"));
            List<Monster_Controller> monsters = colliders.Select(o => o.gameObject.GetComponent<Monster_Controller>()).ToList();

            if (monsters.Count() == 0) { yield return null; }
            go = Managers.Game.objectPool.activateObject(typeof(Base_SkillCast), prefabName);

            BloodyMassacre newScript = gameObject.AddComponent(script) as BloodyMassacre;
            newScript.skill = skill;
            newScript.anime = go.AddComponent<Animator>();
            newScript.anime.runtimeAnimatorController = animeController;

            BoxCollider2D boxCollider = go.AddComponent<BoxCollider2D>();
            boxCollider.isTrigger = true;

            yield return new WaitForSeconds(skill.skillCoolTime);
        }
    }
}