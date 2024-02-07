using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Status_UI : UI_Scene
{
    enum Sliders
    {
        Exp,
        Hp
    }
    enum TMP
    {
        Level
    }
    private void Start()
    {
        init();
        Managers.Game.player.updateStatus += statusUpdate;
    }
    protected override void init()
    {
        base.init();
        bind<Slider>(typeof(Sliders));
        bind<TMP_Text>(typeof(TMP));
        GameObject Exp = get<Slider>((int)Sliders.Exp).gameObject;
        GameObject Hp = get<Slider>((int)Sliders.Hp).gameObject;
        GameObject Level = get<TMP_Text>((int)TMP.Level).gameObject;
    }
    private void statusUpdate()
    {
        get<Slider>((int)Sliders.Exp).value = Managers.Game.player.Exp / Managers.Game.player.necessaryExp;
        get<Slider>((int)Sliders.Hp).value = Managers.Game.player.Hp / Managers.Game.player.MaxHp;
        get<TMP_Text>((int)TMP.Level).text = Managers.Game.player.level.ToString();
    }
}
