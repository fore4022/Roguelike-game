using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Log_UI : UI_Popup
{
    private TextMeshProUGUI log;
    enum TMPro
    { 
        Log
    }
    private void Start()
    {
        Init();
    }
    protected override void Init()
    {
        base.Init();
        bind<TextMeshProUGUI>(typeof(TMPro));

        log = get<TextMeshProUGUI>((int)TMPro.Log);
    }
}