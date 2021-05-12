using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class KeySetting : MonoBehaviour
{
    public KeySettingType type;

    public GameObject Overlay;
    public TMP_Text OverlayText;

    public UnityEvent KeyChangedEvent;

    [SerializeField]
    private TMP_Text displayTitle;

    private KeySettingFSM fsm;

    private void Awake()
    {
        if (KeyChangedEvent == null)
        {
            KeyChangedEvent = new UnityEvent();
        }
    }

    void Start()
    {
        fsm = new KeySettingFSM(this);
        fsm.AddState(new KeySettingIdleState());
        fsm.AddState(new KeySettingQueryState());
        fsm.AddState(new KeySettingWriteState());

        fsm.SetState<KeySettingIdleState>();

        UpdateDisplay();
    }

    void Update()
    {
        fsm.Update();
    }

    public void OnButtonClick()
    {
        fsm.SetState<KeySettingQueryState>();
    }

    public void UpdateDisplay()
    {
        string displayText;

        switch (type)
        {
            case KeySettingType.RotateCW:
                displayText = UIHelpers.KeycodeToText(MainMenuEntry.Settings.RightButton);
                break;

            case KeySettingType.RotateCCW:
                displayText = UIHelpers.KeycodeToText(MainMenuEntry.Settings.LeftButton);
                break;

            case KeySettingType.Accelerate:
                displayText = UIHelpers.KeycodeToText(MainMenuEntry.Settings.AccelerateButton);
                break;

            case KeySettingType.Fire:
                displayText = UIHelpers.KeycodeToText(MainMenuEntry.Settings.FireButton);
                break;

            default:
                displayText = "UNBOUND";
                break;
        }

        displayTitle.text = displayText;
    }

    public void SetDefault()
    {
        switch (type)
        {
            case KeySettingType.RotateCW:
                MainMenuEntry.Settings.RightButton = GameSettingsData.DEFAULT_KEY_ROTATE_CW;
                break;

            case KeySettingType.RotateCCW:
                MainMenuEntry.Settings.LeftButton = GameSettingsData.DEFAULT_KEY_ROTATE_CCW;
                break;

            case KeySettingType.Accelerate:
                MainMenuEntry.Settings.AccelerateButton = GameSettingsData.DEFAULT_KEY_ACCELERATE;
                break;

            case KeySettingType.Fire:
                MainMenuEntry.Settings.FireButton = GameSettingsData.DEFAULT_KEY_FIRE;
                break;
        }
    }
}