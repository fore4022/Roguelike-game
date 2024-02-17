using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BloodMagicBullet : Base_Skill
{   
    public override IEnumerator skillCast()
    {
        List<GameObject> objs = new List<GameObject>();
        while(true)
        {
            fielldtime += Time.deltaTime;
            if(fielldtime > skill.skillCoolTime)
            {
                GameObject go = Managers.Game.objectPool.activateObject(typeof(Base_Skill), prefabName);
                Collider2D[] colliders = Physics2D.OverlapCircleAll(go.transform.position, Camera.main.orthographicSize * 2 * Camera.main.aspect);
                
                go.AddComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load($"Animation/{prefabName}/{prefabName}");
                objs.Add(go);
                fielldtime = 0;
            }
            foreach(GameObject obj in objs)
            {
                obj.transform.position += Vector3.zero * Time.deltaTime;
            }
            yield return null;
        }
    }
}
