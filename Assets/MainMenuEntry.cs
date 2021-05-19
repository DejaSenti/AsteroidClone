using System.IO;
using UnityEngine;

public class MainMenuEntry : MonoBehaviour
{
    public static GameSettings Settings;

    private void Start()
    {
        if (Settings == null)
        {
            Settings = new GameSettings();
        }

        if (File.Exists(GameSettingsData.SETTINGS_FILE_PATH))
        {
            Settings.LoadSettingsJson();
            Settings.ApplyResolution();
            Settings.ApplyScreenLayout();
        }
        else
        {
            Settings.SetDefault();
            Settings.SaveSettingsJson();
        }

        var resolution = GameSettingsData.ScreenResolutions[Settings.ScreenResolution];
        Screen.SetResolution(resolution.Item1, resolution.Item2, Settings.ScreenLayout);
    }
}
