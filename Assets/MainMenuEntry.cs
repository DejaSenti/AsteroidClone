using UnityEngine;

public class MainMenuEntry : MonoBehaviour
{
    public static GameSettings Settings;

    [SerializeField]
    private GameSettings settings;

    private void Start()
    {
        Settings = settings;

        var resolution = GameSettingsData.ScreenResolutions[Settings.ScreenResolution];
        Screen.SetResolution(resolution.Item1, resolution.Item2, Settings.ScreenLayout);
    }
}
