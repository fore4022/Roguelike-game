using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using System.IO;
public class SwipeMenu_UI : UI_Scene
{
    public GameObject storePage;
    public GameObject mainPage;
    public GameObject belongingsPage;
    enum Buttons
    {
        StoreButton,
        MainButton,
        BelongingsButton
    }
    enum Images
    {
        StorePage,
        MainPage,
        BelongingsPage
    }
    enum TMPro
    {
        StoreTxt,
        MainTxt,
        BelongingsTxt
    }
    private void Start()
    {
        init();
        Managers.UI.showSceneUI<Main_UI>("Main");
    }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));

        storePage = get<Image>((int)Images.StorePage).gameObject;
        mainPage = get<Image>((int)Images.MainPage).gameObject;
        belongingsPage = get<Image>((int)Images.BelongingsPage).gameObject;

        GameObject storeButton = get<Button>((int)Buttons.StoreButton).gameObject;
        GameObject mainButton = get<Button>((int)Buttons.MainButton).gameObject;
        GameObject belongingsButton = get<Button>((int)Buttons.BelongingsButton).gameObject;

        AddUIEvent(storeButton, (PointerEventData data) =>
        {

        }, Define.UIEvent.Click);

        AddUIEvent(mainButton, (PointerEventData data) =>
        {

        }, Define.UIEvent.Click);

        AddUIEvent(belongingsButton, (PointerEventData data) =>
        {

        }, Define.UIEvent.Click);
    }
}