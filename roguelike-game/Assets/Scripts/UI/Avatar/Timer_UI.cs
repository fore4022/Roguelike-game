using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class Timer_UI : UI_Scene
{
    enum TMPro
    {
        Timer
    }
    private TextMeshProUGUI timer;
    private void Start() { init(); }
    private void Update() { timer.text = $"{Managers.Game.minute} : {Managers.Game.second}"; }
    protected override void init()
    {
        base.init();
        bind<TextMeshProUGUI>(typeof(TMPro));
        timer = get<TextMeshProUGUI>((int)TMPro.Timer);
    }
}