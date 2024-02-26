using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TouchOfDeath_Cast : Base_SkillCast
{
    private float interval = 1.5f;
    protected override IEnumerator skillCast()
    {
        Collider2D col;
        Vector3 scale;
        Vector3 position;
        float v = Camera.main.orthographicSize - interval;
        float h = v * Camera.main.aspect - interval;
        while (true)
        {
            for(int i = 0; i < skill.numberOfCast; i++)
            {
                TouchOfDeath newScript= go.AddComponent(script) as TouchOfDeath;
                newScript.skill = skill;
                go = Managers.Game.objectPool.activateObject(typeof(Base_SkillCast), prefabName);
                do
                {
                    position = new Vector3(Random.Range(-h, h), Random.Range(-v, v), 0);
                    scale = Vector3.one * Random.Range(1f, skill.skillRange);
                    col = Physics2D.OverlapBox(position, scale, LayerMask.GetMask("Player"));
                } while (col != null);
                newScript.anime = go.AddComponent<Animator>();
                newScript.anime.runtimeAnimatorController = animeController;
                BoxCollider2D boxCollider = go.AddComponent<BoxCollider2D>();
                (go.GetComponent(script) as TouchOfDeath).delayTime = Random.Range(0f, skill.numberOfCast / 10f);
                boxCollider.isTrigger = true;
            }
            yield return new WaitForSeconds(skill.skillCoolTime);
        }
    }
}
