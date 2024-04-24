using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
public class Main_UI : UI_Scene
{
    enum Images
    {
        Start,
        Encyclopedia,
        Quit
    }
    private void Start() { init(); }
    protected override void init()
    {
        base.init();
        bind<Image>(typeof(Images));
        GameObject start = get<Image>((int)Images.Start).gameObject;
        GameObject encyclopedia = get<Image>((int)Images.Encyclopedia).gameObject;
        GameObject quit = get<Image>((int)Images.Quit).gameObject;
    }
}
