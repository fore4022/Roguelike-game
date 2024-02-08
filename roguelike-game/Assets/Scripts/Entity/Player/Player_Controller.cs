using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
public class Player_Controller : Base_Controller
{
    //private Dictionary<string, Base_Skill> acquiredSkill = new();
    public Action updateStatus = null;
    public Action updateStat = null;
    public Item Item;
    public float skillCooldownReduction;
    public float necessaryExp;
    public float shieldAmount;
    public int level;
    private float h;
    private float v;
    protected override void Start()
    {
        init();
        base.Start();
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        Managers.Input.keyAction -= moving;
        Managers.Input.keyAction += moving;
    }
    protected override void init()
    {
        level = 1;
        attackDamage += Item.attackDamage;
        moveSpeed += Item.moveSpeed;
        Hp = maxHp;
        transform.localScale += new Vector3(Item.playerSizeIncrease, Item.playerSizeIncrease, 0);
    }
    protected override void Update()
    {
        base.Update();
        useSkill();
    }
    private void useSkill()
    {

    }
    public void checkExp()
    {
        while(true)
        {
            necessaryExp = (float)(Managers.Game.basicExp + Managers.Game.increaseExp * 1.15 * (level - 1) * ((level - 1) / 50));
            if (exp >= necessaryExp)
            {
                exp -= necessaryExp;
                level++;
                updateStatus.Invoke();
            }
            else { break; }
        }
    }
    public void attacked(float damage)
    {
        hp -= damage;
        updateStatus.Invoke();
    }
    protected override void moving()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical"); 
        if (h != 0 || v != 0) { transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime * h, transform.position.y + moveSpeed * Time.deltaTime * v); }
    }
    protected override void death()
    {
        Managers.Game.stageEnd();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector2(Camera.main.orthographicSize * 2 * Camera.main.aspect, Camera.main.orthographicSize * 2));
    }
}
