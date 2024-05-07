using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Belongings_UI : UI_Scene
{
    enum Buttons
    {

    }
    enum Images
    {

    }
    enum TMPro
    {

    }
    private void Start() { init(); }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));
    }
}
