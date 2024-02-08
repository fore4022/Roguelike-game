using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
public class Player_Controller : Base_Controller
{
    public List<Base_Skill> acquiredSkill = new();
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
    }
    protected override void init()
    {
        rigid.constraints = RigidbodyConstraints2D.FreezeAll;
        Managers.Input.keyAction -= moving;
        Managers.Input.keyAction += moving;
        Managers.Input.keyAction -= dash;
        Managers.Input.keyAction += dash;
        level = 1;
        attackDamage += Item.attackDamage;
        moveSpeed += Item.moveSpeed;
        Hp = maxHp;
        transform.localScale += new Vector3(Item.playerSizeIncrease, Item.playerSizeIncrease, 0);
        anime.runtimeAnimatorController = Managers.Resource.load<RuntimeAnimatorController>($"Animation/{transform.gameObject.name}/{transform.gameObject.name}");
        anime.Play("donwIdle");
    }
    protected override void Update()
    {
        if (anime.GetCurrentAnimatorStateInfo(0).IsName("death")) { return; }
        if(hp <= 0) { anime.Play("death"); }
        if(Input.anyKey == false) { h = v = 0; }
        setAnime();
        useSkill();
    }
    protected override void setAnime()
    {
        if (h != 0) { anime.Play("horizontalMove"); }
        else if (v != 0)
        {
            if (v == 1) { anime.Play("upMove"); }
            else { anime.Play("downMove"); }
        }
        else
        {
            if (anime.GetCurrentAnimatorStateInfo(0).IsName("horizontalMove")) { anime.Play("horizontalIdle"); }
            else if (anime.GetCurrentAnimatorStateInfo(0).IsName("upMove")) { anime.Play("upIdle"); }
            else if (anime.GetCurrentAnimatorStateInfo(0).IsName("downMove")) { anime.Play("downIdle"); }
        }
    }
    private void useSkill()
    {
        foreach(Base_Skill skill in acquiredSkill)
        {
            if (skill.skill.useType)
            {
                if (!skill.castingSkill) { skill.skillCast(); }
            }
            else { skill.skillCast(); }
        }
    }
    public void getLoot(float gold, float exp)
    {
        Gold -= gold;
        Exp -= exp;
        checkExp();
        updateStat.Invoke();
    }
    private void checkExp()
    {
        while(true)
        {
            necessaryExp = (float)(Managers.Game.basicExp + Managers.Game.increaseExp * 1.15 * (level - 1) * ((level - 1) / 50));
            if (exp >= necessaryExp)
            {
                exp -= necessaryExp;
                level++;//choice ui
                updateStatus.Invoke();
                updateStat.Invoke();
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
        if (h != 0 || v != 0) 
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime * h, transform.position.y + moveSpeed * Time.deltaTime * v);
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0) * (h == 1 ? 1 : 0));
        }
    }
    private void dash()
    {

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
