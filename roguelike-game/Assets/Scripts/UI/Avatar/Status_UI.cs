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
    enum TMPro
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
        bind<TextMeshProUGUI>(typeof(TMPro));

        get<Slider>((int)Sliders.Exp).value = 0;
    }
    private void statusUpdate()
    {
        get<Slider>((int)Sliders.Exp).value = Managers.Game.player.Exp / (float)Managers.Game.player.necessaryExp;
        Debug.Log(Managers.Game.player.Exp / (float)Managers.Game.player.necessaryExp);
        //get<Slider>((int)Sliders.Hp).value = Managers.Game.player.Hp / Managers.Game.player.MaxHp;
        //get<TextMeshProUGUI>((int)TMPro.Level).text = Managers.Game.player.level.ToString();
    }
}