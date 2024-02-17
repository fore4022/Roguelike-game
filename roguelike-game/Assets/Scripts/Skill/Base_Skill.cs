using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Base_Skill : MonoBehaviour
{
    public Skill skill;
    protected float fielldtime;
    protected string prefabName;
    protected void Start() { init(); StartCoroutine(skillCast()); }
    private void init()
    {
        skill = (Skill)Resources.Load($"Data/Skill/{this.GetType().Name}");

        prefabName = this.GetType().Name;
    }
    public abstract IEnumerator skillCast();
}
