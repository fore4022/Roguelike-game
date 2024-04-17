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
        Panel3
    }
    enum TMPro
    {
        name1,
        name2,
        name3,
        explain1,
        explain2,
        explain3
    }
    private void Start()
    {
        init();
    }
    private void OnEnable()
    {
        int rand;
        System.Type scriptType = null;
        for(int i = 0; i < 3; i++)
        {
            rand = Random.Range(0, Managers.Game.skills.Count);
            Skill skill = Managers.Game.skills[rand];
            scriptType = System.Type.GetType($"{skill.skillName}_Cast");

            GameObject panel = get<Image>(i).gameObject;
            TextMeshProUGUI name = get<TextMeshProUGUI>(i).gameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI explain = get<TextMeshProUGUI>(i).gameObject.GetComponent<TextMeshProUGUI>();

            name.text = skill.skillName;
            explain.text = skill.explanation;

            AddUIEvent(panel, (PointerEventData data) =>
            {
                
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