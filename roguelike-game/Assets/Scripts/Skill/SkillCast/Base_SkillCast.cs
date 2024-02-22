using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Base_SkillCast : MonoBehaviour
{
    public Skill skill;
    protected string prefabName;
    protected System.Type script;
    protected RuntimeAnimatorController animeController;
    protected void Start()
    {
        init();
        StartCoroutine(skillCast());
    }
    protected virtual void Update() { if(Managers.Game.player.Hp <= 0) { StopAllCoroutines(); } }
    private void init()
    {
        prefabName = this.GetType().Name.Replace("_Cast","");
        skill = (Skill)Resources.Load($"Data/Skill/{prefabName}");
        script = System.Type.GetType(prefabName);
        animeController = (RuntimeAnimatorController)Resources.Load($"Animation/{prefabName}/{prefabName}");
    }
    protected void StopCoroutine() { StopAllCoroutines(); }
    public abstract IEnumerator skillCast();
}
