using System;
using UnityEngine;
[Obsolete]
public abstract class Base_Controller : MonoBehaviour
{
    protected Animator anime;
    protected BoxCollider2D boxCollider;
    protected Rigidbody2D rigid;

    protected virtual void Start()
    {
        anime = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rigid = GetComponent<Rigidbody2D>();

        rigid.gravityScale = 0f;
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    protected abstract void Init();
}
