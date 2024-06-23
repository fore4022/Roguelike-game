using System.Collections;
using UnityEngine;
public abstract class Base_Controller : Stat
{
    protected Animator anime;
    protected BoxCollider2D boxCollider;
    protected Rigidbody2D rigid;

    [SerializeField]
    protected float animatorPlaySpeed;

    protected virtual void Start()
    {
        anime = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();

        rigid.gravityScale = 0f;
    }
    protected virtual IEnumerator Death() { yield return null; }
    protected virtual void Init() { }
    protected virtual void Update() { }
    protected virtual void Moving() { }
    protected virtual void OnDrawGizmosSelected() { }
}
