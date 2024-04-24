using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public abstract class Base_Controller : Stat
{
    protected Animator anime;
    protected BoxCollider2D boxCollider;
    protected Rigidbody2D rigid;
    protected float animatorPlaySpeed;
    protected virtual void Start()
    {
        boxCollider = Util.getOrAddComponent<BoxCollider2D>(transform.gameObject);
        rigid = Util.getOrAddComponent<Rigidbody2D>(transform.gameObject);
        anime = Util.getOrAddComponent<Animator>(transform.gameObject);
        rigid.gravityScale = 0f;
    }
    protected virtual IEnumerator death() { yield return null; }
    protected virtual void init() { }
    protected virtual void Update() { }
    protected virtual void moving() { }
    protected virtual void OnDrawGizmosSelected() { }
}
