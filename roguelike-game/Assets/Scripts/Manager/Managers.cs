using UnityEngine;
public class Managers : MonoBehaviour
{
    public static Managers s_instance;
    public static Managers Instance { get { Init(); return s_instance; } }

    Resource_Manager _resource = new();
    UI_Manager _ui = new();
    Input_Manager _input = new();
    Game_Manager _game = new();
    Database _data = new();
    public static Resource_Manager Resource { get { return Instance._resource; } }
    public static UI_Manager UI { get { return Instance._ui; } }
    public static Input_Manager Input { get { return Instance._input; } }
    public static Game_Manager Game { get { return Instance._game; } }
    public static Database Data { get { return Instance._data; } }

    private void Awake() { _data.Init(); }
    private void Start()
    {
        Data.SetInventory();
        //Data.setUser();
    }
    public void Update() { _input.OnUpdate(); }
    public static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);

            s_instance = go.GetComponent<Managers>();
        }
    }
}
