using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class SwipeMenu_UI : UI_Scene
{
    public float relocationDelay = 0.9f;
    public float pivot = 0f;

    public Vector2 enterPoint;
    public Vector2 direction;

    private RectTransform _swipePanel;
    private TextMeshProUGUI _level;
    private TextMeshProUGUI _gold;

    private float _relocationValue;
    private int _origin;
    enum Buttons
    {
        StoreButton,
        MainButton,
        InventoryButton,
        Etc
    }
    enum Images
    {
        DragAndDropHandler,
        SwipePanel,
        Synthesis
    }
    enum TMPro
    {
        StoreTxt,
        MainTxt,
        BelongingsTxt,
        Level,
        Gold
    }
    enum Sliders
    {
        Exp
    }
    private void OnEnable() { _origin = 0; }
    private void Start()
    {
        Init();

        //this.gameObject.GetComponent<Canvas>().sortingOrder = 10;

        _relocationValue = this.gameObject.GetComponent<RectTransform>().sizeDelta.x;

        Managers.UI.ShowSceneUI<Store_UI>("Store");
        Managers.UI.ShowSceneUI<Main_UI>("Main");
        Managers.UI.ShowSceneUI<Inventory_UI>("Inventory");
    }
    private void Update()
    {
        if(direction != Vector2.zero)
        {
            if (direction.x != 0 && Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                switch (_origin)
                {
                    case 1:
                        if (direction.x < 0) { _swipePanel.position = new Vector3(direction.x + _relocationValue * _origin, 0, 0); }
                        break;
                    case -1:
                        if (direction.x > 0) { _swipePanel.position = new Vector3(direction.x + _relocationValue * _origin, 0, 0); }
                        break;
                    default:
                        _swipePanel.position = new Vector3(direction.x + _relocationValue * _origin, 0, 0);
                        break;
                }
            }
        }
    }
    protected override void Init()
    {
        base.Init();
        bind<Button>(typeof(Buttons));
        bind<Image>(typeof(Images));
        bind<TextMeshProUGUI>(typeof(TMPro));
        bind<Slider>(typeof(Sliders));

        GameObject storeButton = get<Button>((int)Buttons.StoreButton).gameObject;
        GameObject mainButton = get<Button>((int)Buttons.MainButton).gameObject;
        GameObject inventoryButton = get<Button>((int)Buttons.InventoryButton).gameObject;
        GameObject etc = get<Button>((int)Buttons.Etc).gameObject;
        GameObject dragAndDropHandler = get<Image>((int)Images.DragAndDropHandler).gameObject;

        _swipePanel = get<Image>((int)Images.SwipePanel).gameObject.GetComponent<RectTransform>();

        _swipePanel.pivot = new Vector2(pivot, 0);
        _swipePanel.position = Vector2.zero;

        _level = get<TextMeshProUGUI>((int)TMPro.Level);
        //level.text = $"{}";

        _gold = get<TextMeshProUGUI>((int)TMPro.Gold);
        //gold.text = $"{}";

        AddUIEvent(storeButton, (PointerEventData data) =>
        {
            _origin = 1;
            StartCoroutine(Relocation());
        }, Define.UIEvent.Click);

        AddUIEvent(mainButton, (PointerEventData data) =>
        {
            _origin = 0;
            StartCoroutine(Relocation());
        }, Define.UIEvent.Click);

        AddUIEvent(inventoryButton, (PointerEventData data) =>
        {
            _origin = -1;
            StartCoroutine(Relocation());
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
        AddUIEvent(dragAndDropHandler, (PointerEventData data) => { StartCoroutine(Relocation()); }, Define.UIEvent.EndDrag);
    }
    public IEnumerator Relocation()
    {
        if(direction != Vector2.zero)
        {
            if (Mathf.Abs(direction.x) > _relocationValue / 3)
            {
                if (direction.x > 0 && _origin < 1) { _origin++; }
                else if (direction.x < 0 && _origin > -1) { _origin--; }
            }

            while (true)
            {
                if (direction.x > 0)
                {
                    if (Mathf.Lerp(_relocationValue * _origin, _swipePanel.position.x, (_relocationValue * relocationDelay) / _relocationValue) > _relocationValue * _origin) { break; }
                }
                else if (direction.x < 0)
                {
                    if (Mathf.Lerp(_relocationValue * _origin, _swipePanel.position.x, (_relocationValue * relocationDelay) / _relocationValue) < _relocationValue * _origin) { break; }
                }
                else if (_relocationValue - Mathf.Abs((int)Mathf.Lerp(_relocationValue * _origin, _swipePanel.position.x, (_relocationValue * relocationDelay) / _relocationValue)) < 10) { break; }

                _swipePanel.position = new Vector3((int)Mathf.Lerp(_relocationValue * _origin, _swipePanel.position.x, (_relocationValue * relocationDelay) / _relocationValue), 0f, 0f);

                if (direction != Vector2.zero) { direction = Vector2.zero; }

                yield return null;
            }
        }
        else
        {
            while (true)
            {
                if(_relocationValue - Mathf.Abs((int)Mathf.Lerp(_relocationValue * _origin, _swipePanel.position.x, (_relocationValue * relocationDelay) / _relocationValue)) < 10) { break; }

                _swipePanel.position = new Vector3((int)Mathf.Lerp(_relocationValue * _origin, _swipePanel.position.x, (_relocationValue * relocationDelay) / _relocationValue), 0f, 0f);
                
                yield return null;
            }
        }

        _swipePanel.position = new Vector3(_relocationValue * _origin, 0f, 0f);

        direction = Vector2.zero;
    }
}