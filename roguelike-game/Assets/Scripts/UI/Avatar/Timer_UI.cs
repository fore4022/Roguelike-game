using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Timer_UI : UI_Scene
{
    enum TMPro
    {
        Timer
    }
    private void Start() { init(); }
    private void Update() { get<TextMeshProUGUI>((int)TMPro.Timer).text = $"{Managers.Game.minute} : {Managers.Game.second}"; }
    protected override void init()
    {
        base.init();
        bind<TextMeshProUGUI>(typeof(TMPro));
    }
}