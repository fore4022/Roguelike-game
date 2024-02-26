using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Base_SkillCast : MonoBehaviour
{
    public Skill skill;
    protected string prefabName;
    protected System.Type script;
    protected RuntimeAnimatorController animeController;
    protected GameObject go;
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
        prefabName = this.GetType().Name.Replace("_Cast","");
        skill = (Skill)Resources.Load($"Data/Skill/{prefabName}");
        script = System.Type.GetType(prefabName);
        animeController = (RuntimeAnimatorController)Resources.Load($"Animation/{prefabName}/{prefabName}");
    }
    protected abstract IEnumerator skillCast();
}
