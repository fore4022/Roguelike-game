using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class BloodMagicBullet_Cast : Base_SkillCast
{   
    private List<GameObject> objs = new List<GameObject>();
    protected override void Update()
    {
        base.Update();
        foreach(GameObject obj in objs) { obj.transform.position += new Vector3(Mathf.Cos((obj.transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad), Mathf.Sin((obj.transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad)) * Time.deltaTime * 8; }
    }
    public override IEnumerator skillCast()
    {
        while(true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.position, Camera.main.orthographicSize * 2 * Camera.main.aspect, LayerMask.GetMask("Monster"));
            if (colliders == null) { colliders = Physics2D.OverlapCircleAll(player.position, Camera.main.orthographicSize * 2 + 1.5f, LayerMask.GetMask("Monster")); }
            if (colliders == null) { yield return null; }
            GameObject go = Managers.Game.objectPool.activateObject(typeof(Base_SkillCast), prefabName);
            go.transform.position = player.position;
            Vector3 direction = new();
            foreach (Collider2D col in colliders)
            {
                if (direction == new Vector3()) { direction = col.gameObject.transform.position - player.position; }
                else if (direction.sqrMagnitude > (col.gameObject.transform.position - player.position).sqrMagnitude) { direction = col.gameObject.transform.position - player.position; }
            }
            direction = direction.normalized;
            Debug.Log(Mathf.Atan2(direction.y, direction.x) * 45);
            go.transform.rotation = Quaternion.Euler(0, 0, -90 + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            go.AddComponent<Animator>().runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load($"Animation/{prefabName}/{prefabName}");
            objs.Add(go);
            yield return new WaitForSeconds(skill.skillCoolTime);
        }
    }
}
