using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Ignition_Cast : Base_SkillCast
{
    protected override IEnumerator skillCast()
    {
        while (true)
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(Managers.Game.player.gameObject.transform.position, new Vector2(Managers.Game.camera_w, Managers.Game.camera_h), LayerMask.GetMask("Monster"));
            List<Monster_Controller> monsters = colliders.Select(o => o.gameObject.GetComponent<Monster_Controller>()).ToList();
            if(monsters.Count() == 0) { yield return null; }
            go = Managers.Game.objectPool.activateObject(typeof(Base_SkillCast), prefabName);
            Ignition newScript = go.AddComponent(script) as Ignition;
            newScript.skill = skill;
            yield return new WaitForSeconds(skill.skillCoolTime);
        }
    }
}
