using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UIElements;
public class Stat_UI : UI_Popup
{
    enum Buttons
    {
        exit
    }
    enum TMP
    {
        exit
    }
    enum Images
    {

    }
    private void Start()
    {
        init();
        Managers.Game.player.updateStat += statUpdate;
    }
    protected override void init()
    {
        base.init();
        
    }
    private void statUpdate()
    {

    }
}
