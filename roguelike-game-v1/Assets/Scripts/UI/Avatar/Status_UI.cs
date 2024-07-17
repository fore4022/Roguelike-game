using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;
[Obsolete]
public class Status_UI : UI_Scene
{
    private Slider _exp;
    private Slider _hp;
    private TextMeshProUGUI _level;

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
        Init();
        Managers.Game.player.updateStatus -= statusUpdate;
        Managers.Game.player.updateStatus += statusUpdate;
    }
    private void Update()
    {
        if(Managers.Game.player.Hp == Managers.Game.player.MaxHp) { _hp.gameObject.SetActive(false); return; }
        else { _hp.gameObject.SetActive(true); }

        if (Managers.Game.player.enabled)
        {
            pos = Camera.main.WorldToScreenPoint(new Vector2(Managers.Game.player.gameObject.transform.position.x, Managers.Game.player.gameObject.transform.position.y - Managers.Game.player.gameObject.transform.localScale.y));
            _hp.transform.position = pos;
        }

        if (Managers.Game.player.Hp <= 0) { _hp.gameObject.SetActive(false); }
    }
    protected override void Init()
    {
        base.Init();
        bind<Slider>(typeof(Sliders));
        bind<TextMeshProUGUI>(typeof(TMPro));

        _exp = get<Slider>((int)Sliders.Exp);
        _hp = get<Slider>((int)Sliders.Hp);
        _level = get<TextMeshProUGUI>((int)TMPro.Level);

        _exp.interactable = false;
        _hp.interactable = false;

        _exp.value = 0;
        _hp.value = Managers.Game.player.MaxHp;
        _level.text = Managers.Game.player.level.ToString();
    }
    private void statusUpdate()
    {
        _exp.value = Managers.Game.player.Exp / (float)Managers.Game.player.necessaryExp;
        _hp.value = Managers.Game.player.Hp / (float)Managers.Game.player.MaxHp;
        _level.text = Managers.Game.player.level.ToString();
    }
}