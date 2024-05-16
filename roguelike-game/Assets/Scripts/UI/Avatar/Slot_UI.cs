using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class Slot_UI : UI_Scene
{
    enum Images
    {
        Panel,
        Item
    }
    enum TMPro
    {
        Count
    }
    private void Start() { init(); }
    protected override void init()
    {
        base.init();
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));

        GameObject pnael = get<Image>((int)Images.Panel).gameObject;
    }
}
