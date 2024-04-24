using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
public class Main_UI : UI_Scene
{
    enum Buttons
    {
        Start,
        Encyclopedia,
        Quit
    }
    private void Start() { init(); }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        GameObject start = get<Button>((int)Buttons.Start).gameObject;
        GameObject encyclopedia = get<Button>((int)Buttons.Encyclopedia).gameObject;
        GameObject quit = get<Button>((int)Buttons.Quit).gameObject;
    }
}
