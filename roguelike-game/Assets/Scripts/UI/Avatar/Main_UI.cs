using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;
public class Main_UI : UI_Scene
{
    enum Images
    {
        Start,
        Setting
    }
    enum TMPro
    {

    }
    private void Start() { init(); }
    protected override void init()
    {
        base.init();
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));

        GameObject start = get<Image>((int)Images.Start).gameObject;
        GameObject setting = get<Image>((int)Images.Setting).gameObject;

        AddUIEvent(start, (PointerEventData data) => 
        {
            
        }, Define.UIEvent.Click);

        AddUIEvent(setting, (PointerEventData data) =>
        {
            
        }, Define.UIEvent.Click);
    }
}