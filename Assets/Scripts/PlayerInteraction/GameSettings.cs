using System;
using System.IO;
using UnityEngine;

[Serializable]
public class GameSettings
{
    public KeyCode LeftButton;
    public KeyCode RightButton;
    public KeyCode AccelerateButton;
    public KeyCode FireButton;

    public FullScreenMode ScreenLayout;
    public int ScreenResolution;
    public int Difficulty;

    [NonSerialized]
    public bool LeftButtonHeld;
    [NonSerialized]
    public bool RightButtonHeld;
    [NonSerialized]
    public bool AccelerateButtonHeld;
    [NonSerialized]
    public bool FireButtonHeld;


    public void UpdateInput()
    {
        LeftButtonHeld = Input.GetKey(LeftButton);
        RightButtonHeld = Input.GetKey(RightButton);
        AccelerateButtonHeld = Input.GetKey(AccelerateButton);
        FireButtonHeld = Input.GetKey(FireButton);
    }

    public void SetDefault()
    {
        LeftButton = GameSettingsData.DEFAULT_KEY_ROTATE_CCW;
        RightButton = GameSettingsData.DEFAULT_KEY_ROTATE_CW;
        AccelerateButton = GameSettingsData.DEFAULT_KEY_ACCELERATE;
        FireButton = GameSettingsData.DEFAULT_KEY_FIRE;
        ScreenLayout = GameSettingsData.DEFAULT_SCREEN_LAYOUT;
        ScreenResolution = GameSettingsData.DEFAULT_SCREEN_RESOLUTION;
        Difficulty = GameSettingsData.DEFAULT_DIFFICULTY;

        ApplyScreenLayout();
        ApplyResolution();
    }

    public void SaveSettingsJson()
    {
        string settingsJSON = JsonUtility.ToJson(this);
        File.WriteAllText(GameSettingsData.SETTINGS_FILE_PATH, settingsJSON);
    }

    public void LoadSettingsJson()
    {
        JsonUtility.FromJsonOverwrite(File.ReadAllText(GameSettingsData.SETTINGS_FILE_PATH), this);
    }

    public void ApplyScreenLayout(FullScreenMode fullScreenMode)
    {
        ScreenLayout = fullScreenMode;
        Screen.SetResolution(Screen.width, Screen.height, fullScreenMode);
    }

    public void ApplyScreenLayout()
    {
        Screen.SetResolution(Screen.width, Screen.height, ScreenLayout);
    }

    public void ApplyResolution(int resolution)
    {
        ScreenResolution = resolution;

        var resolutionTuple = GameSettingsData.ScreenResolutions[resolution];
        Screen.SetResolution(resolutionTuple.Item1, resolutionTuple.Item2, Screen.fullScreenMode);
    }

    public void ApplyResolution()
    {
        var resolutionTuple = GameSettingsData.ScreenResolutions[ScreenResolution];
        Screen.SetResolution(resolutionTuple.Item1, resolutionTuple.Item2, Screen.fullScreenMode);
    }

    public void ApplyDifficulty(int difficulty)
    {
        Difficulty = difficulty;
    }
}