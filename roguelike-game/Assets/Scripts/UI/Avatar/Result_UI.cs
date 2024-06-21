using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;
[Obsolete]
public class Result_UI : UI_Popup
{
    private GameObject _retry;
    private GameObject _main;
    private GameObject _background;
    private GameObject _resultPanel;
    enum Buttons
    {
        Retry,
        Main
    }
    enum Images
    {
        Background,
        ResultPanel
    }
    enum TMPro
    {

    }
    private void Start() { Init(); }
    protected override void Init()
    {
        base.Init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));

        _retry = get<Button>((int)Buttons.Retry).gameObject;
        _main = get<Button>((int)Buttons.Main).gameObject;
        _background = get<Button>((int)Images.Background).gameObject;
        _resultPanel = get<Button>((int)Images.ResultPanel).gameObject;

        AddUIEvent(_retry, (PointerEventData data) =>
        {

        }, Define.UIEvent.Click);
        AddUIEvent(_main, (PointerEventData data) =>
        {

        }, Define.UIEvent.Click);

        _resultPanel.SetActive(false);

        StartCoroutine(Visible());
    }
    private IEnumerator Visible()
    {
        while(true)
        {
            yield return null;
        }

        //resultPanel.SetActive(true);
    }
}
