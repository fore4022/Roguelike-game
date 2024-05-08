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
    enum Buttons
    {
        StoreButton,
        MainButton,
        BelongingsButton
    }
    enum Images
    {
        DragAndDropHandler
    }
    enum TMPro
    {
        StoreTxt,
        MainTxt,
        BelongingsTxt,
        Gold
    }
    private void Start()
    {
        init();
        Managers.UI.showSceneUI<Store_UI>("Store");
        Managers.UI.showSceneUI<Main_UI>("Main");
        Managers.UI.showSceneUI<Belongings_UI>("Belongings");
    }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));

        GameObject storeButton = get<Button>((int)Buttons.StoreButton).gameObject;
        GameObject mainButton = get<Button>((int)Buttons.MainButton).gameObject;
        GameObject belongingsButton = get<Button>((int)Buttons.BelongingsButton).gameObject;
        GameObject dragAndDropHandler = get<Image>((int)Images.DragAndDropHandler).gameObject;

        AddUIEvent(storeButton, (PointerEventData data) =>
        {

        }, Define.UIEvent.Click);

        AddUIEvent(mainButton, (PointerEventData data) =>
        {

        }, Define.UIEvent.Click);

        AddUIEvent(belongingsButton, (PointerEventData data) =>
        {

        }, Define.UIEvent.Click);

        AddUIEvent(dragAndDropHandler, (PointerEventData data) =>
        {
            
        }, Define.UIEvent.BeginDrag);
        AddUIEvent(dragAndDropHandler, (PointerEventData data) =>
        {
            
        }, Define.UIEvent.Drag);
        AddUIEvent(dragAndDropHandler, (PointerEventData data) =>
        {
            
        }, Define.UIEvent.EndDrag);
    }
}