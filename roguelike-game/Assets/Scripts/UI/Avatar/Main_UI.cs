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
        Help,
        Quit
    }
    private void Start() { init(); }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        GameObject start = get<Button>((int)Buttons.Start).gameObject;
        GameObject help = get<Button>((int)Buttons.Start).gameObject;
        GameObject quit = get<Button>((int)Buttons.Start).gameObject;
        //start
        AddUIEvent(start, (PointerEventData data) =>
        {

        }, Define.UIEvent.Click);
        AddUIEvent(start, (PointerEventData data) =>
        {

        }, Define.UIEvent.Enter);
        //help
        AddUIEvent(help, (PointerEventData data) =>
        {

        }, Define.UIEvent.Click);
        AddUIEvent(help, (PointerEventData data) =>
        {

        }, Define.UIEvent.Enter);
        //quit
        AddUIEvent(quit, (PointerEventData data) =>
        {

        }, Define.UIEvent.Click);
        AddUIEvent(quit, (PointerEventData data) =>
        {
            
        }, Define.UIEvent.Enter);
    }
}
