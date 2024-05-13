using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor.Experimental.GraphView;
public class SwipeMenu_UI : UI_Scene
{
    public float relocationDelay = 0.25f;
    public float pivot = 0f;

    public Vector2 enterPoint;
    public Vector2 direction;

    private float relocationValue;
    private int origin;

    private RectTransform panel;
    enum Buttons
    {
        StoreButton,
        MainButton,
        InventoryButton
    }
    enum Images
    {
        DragAndDropHandler,
        Panel
    }
    enum TMPro
    {
        StoreTxt,
        MainTxt,
        BelongingsTxt
    }
    private void OnEnable() { origin = 0; }
    private void Start()
    {
        init();

        relocationValue = this.gameObject.GetComponent<RectTransform>().sizeDelta.x;

        Managers.UI.showSceneUI<Store_UI>("Store");
        Managers.UI.showSceneUI<Main_UI>("Main");
        Managers.UI.showSceneUI<Inventory_UI>("Inventory");
    }
    private void Update()
    {
        if(direction != Vector2.zero)
        {
            if (direction.x != 0 && Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                switch (origin)
                {
                    case 1:
                        if (direction.x < 0) { panel.position = new Vector3(direction.x + relocationValue * origin, 0, 0); }
                        break;
                    case -1:
                        if (direction.x > 0) { panel.position = new Vector3(direction.x + relocationValue * origin, 0, 0); }
                        break;
                    default:
                        panel.position = new Vector3(direction.x + relocationValue * origin, 0, 0);
                        break;
                }
            }
        }
    }
    protected override void init()
    {
        base.init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));

        GameObject storeButton = get<Button>((int)Buttons.StoreButton).gameObject;
        GameObject mainButton = get<Button>((int)Buttons.MainButton).gameObject;
        GameObject inventoryButton = get<Button>((int)Buttons.InventoryButton).gameObject;
        GameObject dragAndDropHandler = get<Image>((int)Images.DragAndDropHandler).gameObject;

        panel = get<Image>((int)Images.Panel).gameObject.GetComponent<RectTransform>();
        panel.pivot = new Vector2(pivot, 0);
        panel.position = Vector2.zero;

        AddUIEvent(storeButton, (PointerEventData data) =>
        {
            origin = 1;
            StartCoroutine(relocation());
        }, Define.UIEvent.Click);

        AddUIEvent(mainButton, (PointerEventData data) =>
        {
            origin = 0;
            StartCoroutine(relocation());
        }, Define.UIEvent.Click);

        AddUIEvent(inventoryButton, (PointerEventData data) =>
        {
            origin = -1;
            StartCoroutine(relocation());
        }, Define.UIEvent.Click);

        AddUIEvent(dragAndDropHandler, (PointerEventData data) =>
        {
#if UNITY_EDITOR
            enterPoint = Input.mousePosition;
#endif
#if UNITY_ANDROID
            enterPoint = Input.GetTouch(0).position;
#endif
        }, Define.UIEvent.BeginDrag);
        AddUIEvent(dragAndDropHandler, (PointerEventData data) =>
        {
#if UNITY_EDITOR
            direction = (Vector2)Input.mousePosition - enterPoint;
#endif
#if UNITY_ANDROID
            direction = Input.GetTouch(0).position - enterPoint;
#endif
        }, Define.UIEvent.Drag);
        AddUIEvent(dragAndDropHandler, (PointerEventData data) => { StartCoroutine(relocation()); }, Define.UIEvent.EndDrag);
    }
    public IEnumerator relocation()
    {
        float timer = 0;
        
        if(direction != Vector2.zero)
        {
            if (Mathf.Abs(direction.x) > relocationValue / 3)
            {
                if (direction.x > 0 && origin < 1) { origin++; }
                else if (direction.x < 0 && origin > -1) { origin--; }
            }

            while (timer <= (Mathf.Abs(direction.x) / relocationValue))
            {
                if(Mathf.Lerp(relocationValue * origin, panel.position.x, (Mathf.Abs(direction.x) / relocationValue)) > relocationValue * origin) { break; }

                panel.position = new Vector3((int)Mathf.Lerp(relocationValue * origin, panel.position.x, (Mathf.Abs(direction.x) / relocationValue)), 0f, 0f);
                timer += Mathf.Abs(direction.x) / relocationValue;
                yield return null; 
            }
        }
        else
        {
            while (timer <= relocationDelay * 3)
            {
                panel.position = new Vector3((int)Mathf.Lerp(relocationValue * origin, panel.position.x, relocationDelay * 3), 0f, 0f);
                timer += Time.deltaTime;
                yield return null;
            }
        }

        panel.position = new Vector3(relocationValue * origin, 0f, 0f);

        direction = Vector2.zero;
    }
}