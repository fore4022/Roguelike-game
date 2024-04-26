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

    private Item item;

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

        if(Managers.Game.item == null) { item = ScriptableObject.CreateInstance<Item>(); }
        else { item = Managers.Game.item; }

        level = 1;
        maxHp = hp = (int)(100 * item.hp);
        damage = 10 * item.damage;
        skillCooldownReduction = item.skillCooldownReduction;
        MoveSpeed = 2.5f * item.moveSpeed;

        string name = transform.gameObject.name;
        name = name.Replace("(Clone)", "");

        animatorPlaySpeed = 0.4f;
        anime = Util.getOrAddComponent<Animator>(transform.gameObject);
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
    private void setAnime()
    {
        if (h != 0 || v != 0) { anime.Play("move"); }
        else { anime.Play("idle"); }
    }
    public void getLoot(int gold, int exp)
    {
        Gold += (int)(gold * item.goldMagnification);
        Exp += (int)(exp * item.expMagnification);
        checkExp();

        //updateStat.Invoke();
        updateStatus.Invoke();
    }
    private void checkExp()
    {
        necessaryExp = (int)(Managers.Game.basicExp + 15 * (level - 1)) * (1 + level / 8);
        if (exp >= necessaryExp)
        {
            exp -= necessaryExp;
            level++;
            necessaryExp = (int)(Managers.Game.basicExp + 15 * (level - 1)) * (1 + level / 8);
        }
    }
    public void attacked(int damage)
    {
        hp -= damage;
        updateStatus.Invoke();
    }
    protected override void moving()
    {
        #if UNITY_EDITOR
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            if (h != 0 || v != 0) { transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime * h, transform.position.y + moveSpeed * Time.deltaTime * v); }
            if(h != 0) { transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0) * (h < 0 ? 0 : 1)); }
        }
        #endif
        #if UNITY_ANDROID
        {

        }
        #endif
    }
    protected override IEnumerator death()
    {
        anime.Play("death");
        while (true)
        {
            if(anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f && anime.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                Managers.Game.stageEnd();
                break;
            }
            yield return null;
        }
    }
    private void crash(Collision2D collision) { if (collision.gameObject.CompareTag("Monster")) { attacked(collision.gameObject.GetComponent<Monster_Controller>().Damage); } }
    private void OnCollisionEnter2D(Collision2D collision) { crash(collision); }
    private void OnCollisionStay2D(Collision2D collision) { crash(collision); }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector2(Managers.Game.camera_w, Managers.Game.camera_h));
    }
}
