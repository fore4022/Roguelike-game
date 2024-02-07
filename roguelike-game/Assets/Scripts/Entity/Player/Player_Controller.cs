using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
public class Player_Controller : Base_Controller
{
    public Action updateStatus = null;
    public Action updateStat = null;
    public Item Item;
    public List<Skill> skills = new List<Skill>();
    public Action skill = null;
    public float skillCooldownReduction;
    public float necessaryExp;
    public int level;
    private float h;
    private float v;
    protected override void Start()
    {
        init();
        base.Start();
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(Attack());
    }
    protected override void init()
    {
        level = 1;
        attackDamage += Item.attackDamage;
        AttackSpeed = Item.attackSpeed;
        moveSpeed += Item.moveSpeed;
        Hp = maxHp;
        transform.localScale += new Vector3(Item.playerSizeIncrease, Item.playerSizeIncrease, 0);
    }
    protected override void Update()
    {
        base.Update();
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        if (state != State.Death)
        {
            if (h != 0 || v != 0) { state = State.Moving; }
        }
        if (skill != null) { skill.Invoke(); }
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
    private IEnumerator Attack()
    {
        while(true)
        {
            if(state == State.Death)
            {
                yield break;
            }
            yield return new WaitForSeconds(attackSpeed);
        }
    }
    protected override void moving()
    {
        transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime * h, transform.position.y + moveSpeed * Time.deltaTime * v);
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
