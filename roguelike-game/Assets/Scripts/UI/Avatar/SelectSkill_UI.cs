using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Linq;
public class SelectSkill_UI : UI_Popup
{
    enum Images
    {
        Panel1,
        Panel2,
        Panel3,
        Skill1,
        Skill2,
        Skill3
    }
    enum TMPro
    {
        name1,
        name2,
        name3,
        explain1,
        explain2,
        explain3,
        level1,
        level2,
        level3
    }
    private void Start() { init(); }
    private void OnEnable()
    {
        int rand;
        List<Skill> skills = Managers.Game.skills.Where(o => o.skillLevel < 5).ToList();
        for(int i = 0; i < 3; i++)
        {
            rand = Random.Range(0, skills.Count);
            Skill skill = skills[rand];
            skills.Remove(skill);
            System.Type scriptType = System.Type.GetType($"{skill.skillName}_Cast");

            Image panel = get<Image>(i);
            Image skillImage = get<Image>(i);
            TextMeshProUGUI name = get<TextMeshProUGUI>(i).gameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI explain = get<TextMeshProUGUI>(i + 3).gameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI level = get<TextMeshProUGUI>(i + 6).gameObject.GetComponent<TextMeshProUGUI>();

            skillImage.sprite = Managers.Resource.load<Sprite>($"sprites/Icon/{skill.skillName}");
            name.text = skill.skillName;
            explain.text = skill.explanation;
            if(skill.skillLevel == 0) { level.text = "new"; }
            else { level.text = skill.skillLevel.ToString() + "Lv"; }

            AddUIEvent(panel.gameObject, (PointerEventData data) =>
            {
                if(Managers.Game.skill.GetComponent(scriptType) == null) { Managers.Game.skill.AddComponent(scriptType); }
                else 
                {
                    Base_SkillCast cast = Managers.Game.skill.GetComponent(scriptType) as Base_SkillCast;
                    cast.skill.skillLevel++;
                }
                Managers.UI.closePopupUI();
            });
        }
    }
    protected override void init()
    {
        base.init();
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));
    }
}