using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Main_UI : UI_Scene
{
    private TextMeshProUGUI timer;
    private GameObject stageName;
    private GameObject stagePanel;
    enum Buttons
    {
        Start,
        Etc,
        Help
    }
    enum Images
    {
        StagePanel
    }
    enum TMPro
    {
        Timer,
        StageName
    }
    private void Start() 
    {
        init();
        Transform pos = GameObject.Find($"{this.GetType().Name.Replace("_UI", "")}" + "Page").transform;

        RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.offsetMin = new Vector2(0, 0);
        rectTransform.offsetMax = new Vector2(1, 1);
        this.gameObject.transform.SetParent(pos);
    }
    private void Update() { /*timer.text = "";*/ }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));

        GameObject start = get<Button>((int)Buttons.Start).gameObject;
        GameObject etc = get<Button>((int)Buttons.Etc).gameObject;
        GameObject help = get<Button>((int)Buttons.Help).gameObject;

        //stagePanel = get<Image>((int)Images.StagePanel).gameObject;
        //timer = get<TextMeshProUGUI>((int)TMPro.Timer);
        //stageName = get<TextMeshProUGUI>((int)TMPro.StageName).gameObject;
    }
}
