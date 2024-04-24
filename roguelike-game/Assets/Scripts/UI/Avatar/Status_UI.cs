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
    private void Start()
    {
        init();
        Managers.Game.player.updateStatus += statusUpdate;

        get<Slider>((int)Sliders.Exp).value = 0;
        get<Slider>((int)Sliders.Hp).value = Managers.Game.player.MaxHp;
        get<TextMeshProUGUI>((int)TMPro.Level).text = Managers.Game.player.level.ToString();
    }
    private void Update()
    {
        pos = Camera.main.WorldToScreenPoint(new Vector2(Managers.Game.player.gameObject.transform.position.x, Managers.Game.player.gameObject.transform.position.y - Managers.Game.player.gameObject.transform.localScale.y));
        get<Slider>((int)Sliders.Hp).gameObject.transform.position = pos;
    }
    protected override void init()
    {
        base.init();
        bind<Slider>(typeof(Sliders));
        bind<TextMeshProUGUI>(typeof(TMPro));
    }
    private void statusUpdate()
    {
        get<Slider>((int)Sliders.Exp).value = Managers.Game.player.Exp / (float)Managers.Game.player.necessaryExp;
        get<Slider>((int)Sliders.Hp).value = Managers.Game.player.Hp / (float)Managers.Game.player.MaxHp;
        get<TextMeshProUGUI>((int)TMPro.Level).text = Managers.Game.player.level.ToString();
    }
}