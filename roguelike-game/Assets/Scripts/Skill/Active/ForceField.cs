using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ForceField : Base_Skill
{
    protected override void init()
    {
        Collider2D[] colliders;
        Collider2D collider;
        int maxCount = 0;
        colliders = Physics2D.OverlapBoxAll(Managers.Game.player.gameObject.transform.position, new Vector2(Managers.Game.camera_h, Managers.Game.camera_v), LayerMask.GetMask("Monster"));
        foreach (Collider2D col in colliders)
        {
            if (maxCount < col.gameObject.GetComponent<Monster_Controller>().monsterCount())
            {
                maxCount = col.gameObject.GetComponent<Monster_Controller>().monsterCount();
                collider = col;
            }
        }
    }
}
