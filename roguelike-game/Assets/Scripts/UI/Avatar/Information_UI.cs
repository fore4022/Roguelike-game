using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Information_UI : UI_Scene
{
    private Slider exp;
    private TextMeshProUGUI level;
    private TextMeshProUGUI gold;
    enum Sliders
    {
        Exp
    }
    enum TMPro
    {
        Level,
        Gold
    }
    private void Start() 
    {
        init();
        //action
    }
    protected override void init()
    {
        base.init();
        bind<Slider>(typeof(Sliders));
        bind<TextMeshProUGUI>(typeof(TMPro));

        exp = get<Slider>((int)Sliders.Exp);
        level = get<TextMeshProUGUI>((int)TMPro.Level);
        gold = get<TextMeshProUGUI>((int)TMPro.Gold);
    }
    public void informationUpdate()
    {
        //exp.value =;user exp, Level...
        //gold = ;
    }
}