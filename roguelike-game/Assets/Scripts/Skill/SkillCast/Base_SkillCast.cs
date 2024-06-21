using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public abstract class Base_SkillCast : MonoBehaviour
{
    public Skill skill;

    protected System.Type script;
    protected RuntimeAnimatorController animeController;
    protected GameObject go;

    protected string skillName;
    protected void Start()
    {
        init();
        StartCoroutine(skillCast());

        Managers.Game.player.updateStatus -= stop;
        Managers.Game.player.updateStatus += stop;
    }
    private void stop()
    {
        if(Managers.Game.player.Hp <= 0)
        {
            Managers.Game.player.updateStatus -= stop;

            this.gameObject.SetActive(false); 
        }
    }
    private void init()
    {
        skillName = this.GetType().Name.Replace("_Cast","");
        skill = (Skill)Resources.Load($"Data/Skill/{skillName}");
        script = System.Type.GetType(skillName);
        animeController = Managers.Resource.load<RuntimeAnimatorController>($"Animation/Skill/{skillName}/{skillName}");
    }
    protected abstract IEnumerator skillCast();
}