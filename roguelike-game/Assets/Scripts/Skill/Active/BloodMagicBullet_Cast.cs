using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class BloodMagicBullet_Cast : Base_SkillCast
{   
    private List<GameObject> objs = new List<GameObject>();
    private void Update()
    {
        foreach(GameObject obj in objs) { obj.transform.position += obj.transform.forward * Time.deltaTime; }
    }
    public override IEnumerator skillCast()
    {
        while(true)
        {
            GameObject go = Managers.Game.objectPool.activateObject(typeof(Base_SkillCast), prefabName);
            go.transform.position = player.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(go.transform.position, Camera.main.orthographicSize * 2 * Camera.main.aspect, LayerMask.NameToLayer("Monster"));
            if (colliders == null) { colliders = Physics2D.OverlapCircleAll(go.transform.position, Camera.main.orthographicSize * 2 + 1.5f, LayerMask.NameToLayer("Monster")); }
            Vector3 direction = new();
            Debug.Log(colliders.Count());
            foreach (Collider2D col in colliders)
            {
                Debug.Log(col.gameObject.name);
                if (direction == new Vector3()) { direction = col.gameObject.transform.position - player.position; }
                else if (direction.sqrMagnitude > (col.gameObject.transform.position - player.position).sqrMagnitude) { direction = col.gameObject.transform.position - player.position; }
            }
            Debug.Log(direction);
            direction = direction.normalized;
            go.transform.rotation = Quaternion.Euler(0, 180, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            go.AddComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load($"Animation/{prefabName}/{prefabName}");
            objs.Add(go);
            yield return new WaitForSeconds(skill.skillCoolTime);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.transform.position, Camera.main.orthographicSize * 2 * Camera.main.aspect);
    }
}
