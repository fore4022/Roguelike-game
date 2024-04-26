using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Controller_UI : UI_Popup
{
    private GameObject controller;
    private GameObject controllUnit;
    enum Images
    {
        Controller,
        ControllUnit
    }
    private void Start() { init(); }
    private void OnEnable()
    {
        //controller;
    }
    private void Update()
    {
        //controllUnit.transform.position = ;
    }
    protected override void init()
    {
        base.init();
        bind<Image>(typeof(Images));

        controller = get<Image>((int)Images.Controller).gameObject;
        controllUnit = get<Image>((int)Images.ControllUnit).gameObject;
    }
}
