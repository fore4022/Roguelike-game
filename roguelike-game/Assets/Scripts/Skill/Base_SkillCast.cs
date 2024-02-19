using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Base_SkillCast : MonoBehaviour
{
    public Skill skill;
    protected string prefabName;
    protected Transform player;
    protected void Start() { init(); StartCoroutine(skillCast()); }
    private void init()
    {
        prefabName = this.GetType().Name.Replace("_Cast","");
        skill = (Skill)Resources.Load($"Data/Skill/{prefabName}");
        player = Managers.Game.player.gameObject.transform;
    }
    public abstract IEnumerator skillCast();
}
