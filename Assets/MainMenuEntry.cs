using UnityEngine;

public class MainMenuEntry : MonoBehaviour
{
    private void Start()
    {
        var resolution = GameSettingsData.ScreenResolutions[UISettings.Settings.ScreenResolution];
        Screen.SetResolution(resolution.Item1, resolution.Item2, UISettings.Settings.ScreenLayout);
    }
}
