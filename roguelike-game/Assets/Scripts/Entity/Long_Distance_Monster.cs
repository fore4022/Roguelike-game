using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Long_Distance_Monster : Monster_Controller
{
    protected bool isAttack = false;
    protected override void Update()
    {
        base.Update();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Player"));
        if(colliders != null)
        {
            isAttack = true;
        }
        else
        {
            isAttack = false;
        }
    }
}
