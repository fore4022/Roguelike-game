using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Status_UI : UI_Scene
{
    private Vector2 pos;
    enum Sliders
    {
        Exp,
        Hp
    }
    enum TMPro
    {
        Level
    }
    private Slider exp;
    private Slider hp;
    private TextMeshProUGUI level;
    private void Start()
    {
        init();
        Managers.Game.player.updateStatus -= statusUpdate;
        Managers.Game.player.updateStatus += statusUpdate;
    }
    private void Update()
    {
        if (Managers.Game.player.enabled)
        {
            pos = Camera.main.WorldToScreenPoint(new Vector2(Managers.Game.player.gameObject.transform.position.x, Managers.Game.player.gameObject.transform.position.y - Managers.Game.player.gameObject.transform.localScale.y));
            hp.transform.position = pos;
        }
        if (Managers.Game.player.Hp <= 0) { hp.gameObject.SetActive(false); }
    }
    protected override void init()
    {
        base.init();
        bind<Slider>(typeof(Sliders));
        bind<TextMeshProUGUI>(typeof(TMPro));

        exp = get<Slider>((int)Sliders.Exp);
        hp = get<Slider>((int)Sliders.Hp);
        level = get<TextMeshProUGUI>((int)TMPro.Level);

        exp.value = 0;
        hp.value = Managers.Game.player.MaxHp;
        level.text = Managers.Game.player.level.ToString();
    }
    private void statusUpdate()
    {
        exp.value = Managers.Game.player.Exp / (float)Managers.Game.player.necessaryExp;
        hp.value = Managers.Game.player.Hp / (float)Managers.Game.player.MaxHp;
        level.text = Managers.Game.player.level.ToString();
    }
}