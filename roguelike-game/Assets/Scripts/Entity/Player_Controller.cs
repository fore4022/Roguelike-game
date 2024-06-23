using System;
using System.Collections;
using UnityEngine;
[Obsolete]
public class Player_Controller : Base_Controller
{
#if UNITY_ANDROID
    public Vector2 enterPoint;
#endif

    public Action updateStatus = null;
    public Action updateStat = null;

    private Vector2 _direction;

    public float skillCooldownReduction;
    public float shieldAmount;

    private float _horizontal;
    private float _vertical;

    public int necessaryExp;
    public int level;

    protected override void Start()
    {
        base.Start();

        Init();
    }
    protected override void Init()
    {
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;

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
        skillCooldownReduction = 0;
        moveSpeed = 2;

        animatorPlaySpeed = 0.4f;

        //anime.runtimeAnimatorController = Managers.Resource.Load<RuntimeAnimatorController>($"Animation/{name}/{name}")
        anime.speed = animatorPlaySpeed;
    }
    protected override void Update()
    {
        if (anime.GetCurrentAnimatorStateInfo(0).IsName("death")) { return; }//

        if (hp <= 0) 
        { 
            Managers.Input.keyAction -= Moving;

            _horizontal = _vertical = 0;

            StartCoroutine(Death());
        }

        if (Input.anyKey == false) { _horizontal = _vertical = 0; }

        SetAnime();
    }
    private void SetAnime()
    {
        if (_horizontal != 0 || _vertical != 0) { anime.Play("move"); }
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
    protected override void Moving()    
    {
#if UNITY_EDITOR
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical");
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

                _horizontal = _direction.x;
                _vertical = _direction.y;
            }
        }
#endif
        if (_horizontal != 0 || _vertical != 0) { transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime * _horizontal, transform.position.y + moveSpeed * Time.deltaTime * _vertical); }

        if (_horizontal != 0) { transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0) * (_horizontal < 0 ? 0 : 1)); }
    }
    protected override IEnumerator Death()
    {
        anime.Play("death");

        while (true)
        {
            if(anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f && anime.GetCurrentAnimatorStateInfo(0).IsName("death"))
            {
                for (; ; )
                {
                    if(Managers.UI.SceneStack.Count > 0) { Managers.UI.CloseSceneUI(); }
                    else { break; }
                }

                Managers.Game.StageEnd();
                //show
                break;
            }

            yield return null;
        }
    }
    private void Crash(Collision2D collision) { if (collision.gameObject.CompareTag("Monster")) { Attacked(collision.gameObject.GetComponent<Monster_Controller>().Damage); } }
    private void OnCollisionEnter2D(Collision2D collision) { Crash(collision); }
    private void OnCollisionStay2D(Collision2D collision) { Crash(collision); }
    private void OnDestroy() { Managers.Input.keyAction -= Moving; }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector2(Managers.Game.camera_w, Managers.Game.camera_h));
    }
}
