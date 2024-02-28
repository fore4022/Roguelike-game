using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
public class CorpseExplosion_Cast : Base_SkillCast
{
    protected override IEnumerator skillCast()
    {
        while(true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Camera.main.orthographicSize * 2 * Camera.main.aspect, LayerMask.GetMask("Monster"));
            List<Monster_Controller> monsters = colliders.Select(o => o.gameObject.GetComponent<Monster_Controller>().Hp <= 0 ? o.gameObject.GetComponent<Monster_Controller>() : null).ToList();
            monsters.RemoveAll(o => o == null);
            if(monsters.Count() == 0) { yield return null; }
            int rand;
            for(int i = 0; i < skill.numberOfCast; i++)
            {
                rand = Random.Range(0, monsters.Count() + 1);
                go = Managers.Game.objectPool.activateObject(typeof(Base_SkillCast), prefabName);
                go.gameObject.transform.position = colliders[rand].gameObject.transform.position;
                monsters.Remove(monsters[rand]);
                CorpseExplosion newScript = go.AddComponent(script) as CorpseExplosion;
                newScript.skill = skill;
                newScript.anime = go.AddComponent<Animator>();
                newScript.anime.runtimeAnimatorController = animeController;
                BoxCollider2D boxCollider = go.AddComponent<BoxCollider2D>();
                boxCollider.isTrigger = true;
            }
            yield return new WaitForSeconds(skill.skillCoolTime);
        }
    }
}
