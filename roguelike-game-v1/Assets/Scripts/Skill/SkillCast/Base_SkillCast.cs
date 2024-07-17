using System.Collections;
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
        Init();
        StartCoroutine(SkillCast());

        Managers.Game.player.updateStatus -= Stop;
        Managers.Game.player.updateStatus += Stop;
    }
    private void Stop()
    {
        if(Managers.Game.player.Hp <= 0)
        {
            Managers.Game.player.updateStatus -= Stop;

            this.gameObject.SetActive(false); 
        }
    }
    private void Init()
    {
        skillName = this.GetType().Name.Replace("_Cast","");
        skill = (Skill)Resources.Load($"Data/Skill/{skillName}");
        script = System.Type.GetType(skillName);
        animeController = Managers.Resource.Load<RuntimeAnimatorController>($"Animation/Skill/{skillName}/{skillName}");
    }
    protected abstract IEnumerator SkillCast();
}