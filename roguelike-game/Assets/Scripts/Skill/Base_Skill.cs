using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Base_Skill : MonoBehaviour
{
    public Skill skill;
    public Animator anime;
    protected virtual void Start() { init(); }
    protected virtual void init() { }
    protected virtual void Update() { }
}
