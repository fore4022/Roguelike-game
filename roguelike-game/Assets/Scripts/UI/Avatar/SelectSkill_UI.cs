using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class SelectSkill_UI : UI_Popup
{
    enum Buttons
    {
        SKill1,
        Skill2,
        Skill3,
        Skill4,
        Skill5
    }
    enum Images
    {
        Panel1,
        Panel2,
        Panel3,
        Panel4,
        Panel5
    }
    enum TMPro
    {
        name1,
        name2,
        name3,
        name4,
        name5,
        explain1,
        explain2,
        explain3,
        explain4,
        explain5
    }
    private void Start()
    {
        init();
    }
    private void Set()
    {
        int rand;
        Skill skill;
        System.Type scriptType = null;
        for(int i = 0; i < Managers.Game.a; i++)
        {
            rand = Random.Range(0, Managers.Game.skills.Count);
            skill = Managers.Game.skills[rand];
            GameObject button = get<Button>(i).gameObject;
            GameObject panel = get<Image>(i).gameObject;
            TextMeshProUGUI name = get<TextMeshProUGUI>(i).gameObject.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI explain = get<TextMeshProUGUI>(i).gameObject.GetComponent<TextMeshProUGUI>();
        }
    }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));
        Set();
    }
}
