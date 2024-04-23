using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
public class Player_Controller : Base_Controller
{
    public Action updateStatus = null;
    public Action updateStat = null;
    public Item Item;

    public float skillCooldownReduction;
    public float shieldAmount;
    public float h;
    public float v;

    public int necessaryExp;
    public int level;
    protected override void Start()
    {
        base.Start();
        init();
    }
    protected override void init()
    {
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        Managers.Input.keyAction -= moving;
        Managers.Input.keyAction += moving;

        boxCollider.size = new Vector2(0.9f, 1.7f);

        level = 1;
        hp = 100;
        MoveSpeed = 2.5f;
        animatorPlaySpeed = 0.4f;
        //attackDamage += Item.attackDamage;
        //moveSpeed += Item.moveSpeed;
        //Hp = maxHp;
        //transform.localScale += new Vector3(Item.playerSizeIncrease, Item.playerSizeIncrease, 0);

        string name = transform.gameObject.name;
        name = name.Replace("(Clone)", "");
        anime.runtimeAnimatorController = Managers.Resource.load<RuntimeAnimatorController>($"Animation/{name}/{name}");
        anime.speed = animatorPlaySpeed;
    }
    protected override void Update()
    {
        if (anime.GetCurrentAnimatorStateInfo(0).IsName("death")) { return; }
        if (hp <= 0) 
        { 
            Managers.Input.keyAction -= moving;
            h = v = 0;
            StartCoroutine(death());
        }
        if (Input.anyKey == false) { h = v = 0; }
        setAnime();
    }
    protected override void setAnime()
    {
        if (h != 0 || v != 0) { anime.Play("move"); }
        else { anime.Play("idle"); }
    }
    public void getLoot(int gold, int exp)
    {
        Gold -= gold;
        Exp -= exp;
        checkExp();
        //updateStat.Invoke();
    }
    private void checkExp()
    {
        while (true)
        {
            necessaryExp = (int)(Managers.Game.basicExp + Managers.Game.increaseExp * 1.15 * (level - 1) * ((level - 1) / 50));
            if (exp >= necessaryExp)
            {
                exp -= necessaryExp;
                level++;
                updateStatus.Invoke();
                updateStat.Invoke();
            }
            else { break; }
        }
    }
    public void attacked(int damage)
    {
        hp -= damage;
        updateStatus.Invoke();
    }
    protected override void moving()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        if (h != 0 || v != 0) { transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime * h, transform.position.y + moveSpeed * Time.deltaTime * v); }
        if(h != 0) { transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0) * (h < 0 ? 0 : 1)); }
    }
    protected override IEnumerator death()
    {
        anime.Play("death");
        while (true)
        {
            if(anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f && anime.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                Destroy(this.gameObject);
                Managers.Game.stageEnd();
                break;
            }
            yield return null;
        }
    }
    private void crash(Collision2D collision) { if (collision.gameObject.CompareTag("Monster")) { attacked(collision.gameObject.GetComponent<Monster_Controller>().AttackDamage); } }
    private void OnCollisionEnter2D(Collision2D collision) { crash(collision); }
    private void OnCollisionStay2D(Collision2D collision) { crash(collision); }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector2(Managers.Game.camera_w, Managers.Game.camera_h));
    }
}
