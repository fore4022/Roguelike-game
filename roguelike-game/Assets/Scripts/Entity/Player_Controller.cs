using System;
using System.Collections;
using UnityEngine;
public class Player_Controller : Base_Controller
{
    [SerializeField]
    private float animatorPlaySpeed = 0.4f;

#if UNITY_ANDROID
    public Vector2 enterPoint;
#endif

    public Action updateStatus = null;
    public Action updateStat = null;

    private Vector2 _direction;

    public float h;
    public float v;

    public int necessaryExp;
    public int level;

    protected override void Start()
    {
        base.Start();
        Init();
    }
    protected override void Init()
    {
        Managers.Input.keyAction -= Moving;
        Managers.Input.keyAction += Moving;

        boxCollider.size = new Vector2(0.9f, 1.7f);

        level = 1;
        //maxHp = hp = (int)(100 * item.hp);
        //damage = 10 * item.damage;
        //skillCooldownReduction = item.skillCooldownReduction;
        //MoveSpeed = 2 * item.moveSpeed;
        maxHp = hp = (int)(100);
        damage = 10;
        moveSpeed = 2;

        string name = transform.gameObject.name;
        name = name.Replace("(Clone)", "");

        anime.speed = animatorPlaySpeed;
    }
    private void Update()//
    {
        if (anime.GetCurrentAnimatorStateInfo(0).IsName("death")) { return; }

        if (hp <= 0) 
        { 
            Managers.Input.keyAction -= Moving;

            h = v = 0;

            //Death();
        }

        if (Input.anyKey == false) { h = v = 0; }

        SetAnime();
    }
    private void SetAnime()
    {
        if (h != 0 || v != 0) { anime.Play("move"); }
        else { anime.Play("idle"); }
    }
    public void GetLoot(int gold, int exp)
    {
        //Gold += (int)(gold * item.goldMagnification);
        //Exp += (int)(exp * item.expMagnification);
        Gold += (int)(gold);
        Exp += (int)(exp);

        CheckExp();

        //updateStat.Invoke();
        updateStatus.Invoke();
    }
    private void CheckExp()
    {
        necessaryExp = (int)(Managers.Game.basicExp + 15 * (level - 1)) * (1 + level / 8);

        if (exp >= necessaryExp)
        {
            exp -= necessaryExp;
            level++;
            necessaryExp = (int)(Managers.Game.basicExp + 15 * (level - 1)) * (1 + level / 8);
        }
    }
    public void Attacked(int damage)
    {
        hp -= damage;
        updateStatus.Invoke();
    }
    private void Moving()    
    {
#if UNITY_EDITOR
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }
#endif
#if UNITY_ANDROID
        {
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    enterPoint = Input.GetTouch(0).position;

                    return;
                }

                _direction = (Input.GetTouch(0).position - enterPoint).normalized;

                h = _direction.x;
                v = _direction.y;
            }
        }
#endif
        if (h != 0 || v != 0) { transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime * h, transform.position.y + moveSpeed * Time.deltaTime * v); }

        if (h != 0) { transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0) * (h < 0 ? 0 : 1)); }
    }
    private void Death()
    {
        anime.Play("death");

        //while (true)
        //{
        //    if(anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f && anime.GetCurrentAnimatorStateInfo(0).IsName("death"))
        //    {
        //        for (; ; )
        //        {
        //            if(Managers.UI.SceneStack.Count > 0) { Managers.UI.CloseSceneUI(); }
        //            else { break; }
        //        }

        //        Managers.Game.StageEnd();
        //        //show
        //        break;
        //    }

        //    yield return null;
        //}
    }
    private void Crash(Collision2D collision) { if (collision.gameObject.CompareTag("Monster")) { Attacked(collision.gameObject.GetComponent<Monster_Controller>().Damage); } }
    private void OnCollisionEnter2D(Collision2D collision) { Crash(collision); }
    private void OnCollisionStay2D(Collision2D collision) { Crash(collision); }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector2(Managers.Game.camera_w, Managers.Game.camera_h));
    }
}
