using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Base_Skill : MonoBehaviour
{
    public Skill skill;
    protected Animator anime;
    protected void Start() { init(); StartCoroutine(skillCast()); }
    protected virtual void init() { }

    public abstract IEnumerator skillCast();
}
