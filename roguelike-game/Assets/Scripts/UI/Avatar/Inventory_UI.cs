using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
public class Inventory_UI : UI_Scene
{
    private UI_Base swipeMenu;
    enum Buttons
    {

    }
    enum Images
    {
        Panel,
        Panel1,
        Panel2
    }
    enum Sliders
    {

    }
    enum TMPro
    {
        Gold
    }
    private void Start() 
    {
        init();

        if (SceneManager.GetActiveScene().name == "Main")
        {
            Transform pos = GameObject.Find($"{this.GetType().Name.Replace("_UI", "")}" + "Page").transform;
            this.gameObject.transform.SetParent(pos);
            RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();

            rectTransform.sizeDelta = pos.GetComponentInParent<RectTransform>().rect.size;
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;

            swipeMenu = FindObjectOfType<SwipeMenu_UI>();
        }
        else if(SceneManager.GetActiveScene().name == "InGame")
        {
            Transform pos = GameObject.Find($"{this.GetType().Name.Replace("_UI", "")}" + "Page").transform;
            this.gameObject.transform.SetParent(pos);
            RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();

            rectTransform.sizeDelta = pos.GetComponentInParent<RectTransform>().rect.size;
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;

            //swipeMenu = FindObjectOfType<SwipeMenu_UI>(); ?
        }
    }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<Slider>(typeof(Sliders));
        bind<TextMeshProUGUI>(typeof(TMPro));
    }
}
