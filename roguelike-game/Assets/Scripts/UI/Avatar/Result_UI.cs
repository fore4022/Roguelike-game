using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class Result_UI : UI_Popup
{
    private GameObject retry;
    private GameObject main;
    private GameObject background;
    private GameObject resultPanel;
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

        retry = get<Button>((int)Buttons.Retry).gameObject;
        main = get<Button>((int)Buttons.Main).gameObject;
        background = get<Button>((int)Images.Background).gameObject;
        resultPanel = get<Button>((int)Images.ResultPanel).gameObject;

        AddUIEvent(retry, (PointerEventData data) =>
        {

        }, Define.UIEvent.Click);
        AddUIEvent(main, (PointerEventData data) =>
        {

        }, Define.UIEvent.Click);

        resultPanel.SetActive(false);

        StartCoroutine(visible());
    }
    private IEnumerator visible()
    {
        while(true)
        {
            yield return null;
        }

        //resultPanel.SetActive(true);
    }
}
