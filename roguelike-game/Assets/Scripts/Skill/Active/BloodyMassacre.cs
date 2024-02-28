using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class BloodyMassacre : Base_Skill
{
    public Animator anime;
    private Vector3 direction;
    private float projectileSpeed;
    protected override void init()
    {
        Transform player = Managers.Game.player.gameObject.transform;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.position, Managers.Game.camera_v + 1.5f, LayerMask.GetMask("Monster"));
        if (colliders.Count() == 0) { return; }
        direction = new();
        foreach (Collider2D col in colliders)
        {
            if (direction == new Vector3()) { direction = col.gameObject.transform.position - player.position; }
            else if (direction.sqrMagnitude > (col.gameObject.transform.position - player.position).sqrMagnitude) { direction = col.gameObject.transform.position - player.position; }
        }
        direction = direction.normalized;
        transform.position = player.position;
        transform.rotation = Quaternion.Euler(0, 0, -90 + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        projectileSpeed = 12f;
    }
    protected override void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
