using TMPro;
public class Log_UI : UI_Popup
{
    private TextMeshProUGUI _log;
    enum TMPro
    { 
        Log
    }
    private void Start()
    {
        Init();
    }
    protected override void Init()
    {
        base.Init();
        bind<TextMeshProUGUI>(typeof(TMPro));

        _log = get<TextMeshProUGUI>((int)TMPro.Log);
    }
}