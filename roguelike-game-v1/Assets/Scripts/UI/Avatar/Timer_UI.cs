using TMPro;
public class Timer_UI : UI_Scene
{
    enum TMPro
    {
        Timer
    }
    private TextMeshProUGUI _timer;
    private void Start() { Init(); }
    protected override void Init()
    {
        base.Init();
        bind<TextMeshProUGUI>(typeof(TMPro));
        _timer = get<TextMeshProUGUI>((int)TMPro.Timer);
    }
}