using System.Collections;
using UnityEngine;
public abstract class Base_Controller : Stat
{
    protected Animator anime;
    protected BoxCollider2D boxCollider;
    protected Rigidbody2D rigid;

    protected float animatorPlaySpeed;

    protected virtual void Start()
    {
        boxCollider = Util.GetOrAddComponent<BoxCollider2D>(transform.gameObject);
        rigid = Util.GetOrAddComponent<Rigidbody2D>(transform.gameObject);
        rigid.gravityScale = 0f;
    }
    protected virtual IEnumerator Death() { yield return null; }
    protected virtual void Init() { }
    protected virtual void Update() { }
    protected virtual void Moving() { }
    protected virtual void OnDrawGizmosSelected() { }
}
