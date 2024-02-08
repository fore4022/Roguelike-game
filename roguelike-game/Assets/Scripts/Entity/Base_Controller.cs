using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public abstract class Base_Controller : Stat
{
    protected BoxCollider2D boxCollider;
    protected Rigidbody2D rigid;
    protected Animator anime;
    protected virtual void Start()
    {
        boxCollider = Util.getOrAddComponent<BoxCollider2D>(transform.gameObject);
        rigid = Util.getOrAddComponent<Rigidbody2D>(transform.gameObject);
        anime = Util.getOrAddComponent<Animator>(transform.gameObject);
        rigid.gravityScale = 0f;
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    protected virtual void init() { }
    protected virtual void Update() { }
    protected virtual void setAnime() { }
    protected virtual void moving() { }
    protected virtual void death() { }
    protected virtual void OnDrawGizmosSelected() { }
}
