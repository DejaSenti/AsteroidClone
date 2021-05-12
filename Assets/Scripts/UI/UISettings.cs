using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using static TMPro.TMP_Dropdown;

public class UISettings : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private GameObject mainOverlay;

    [SerializeField]
    private TMP_Text rotateCW;
    [SerializeField]
    private TMP_Text rotateCCW;
    [SerializeField]
    private TMP_Text accelerate;
    [SerializeField]
    private TMP_Text shoot;

    [SerializeField]
    private TMP_Dropdown screenLayout;
    [SerializeField]
    private TMP_Dropdown screenResolution;

    [SerializeField]
    private Slider difficulty;
#pragma warning restore 0649

    private List<OptionData> screenLayoutOptions = new List<OptionData>();
    private List<OptionData> screenResolutionOptions = new List<OptionData>();

    private GameSettings settings;

    private void Awake()
    {
        settings = MainMenuEntry.Settings;

        if (screenLayoutOptions.Count == 0)
        {
            foreach(var option in GameSettingsData.LayoutNameByType)
            {
                var newOption = new OptionData(option.Value);
                screenLayoutOptions.Add(newOption);
            }

            screenLayout.options = screenLayoutOptions;
        }

        if (screenResolutionOptions.Count == 0)
        {
            foreach(var option in GameSettingsData.ScreenResolutions)
            {
                var text = UIHelpers.ResolutionTupleToText(option);
                var newOption = new OptionData(text);
                screenResolutionOptions.Add(newOption);
            }

            screenResolution.options = screenResolutionOptions;
        }

        ReadValuesFromSettings();
    }

    private void ReadValuesFromSettings()
    {
        rotateCW.text = UIHelpers.KeycodeToChar(settings.RightButton);
        rotateCCW.text = UIHelpers.KeycodeToChar(settings.LeftButton);
        accelerate.text = UIHelpers.KeycodeToChar(settings.AccelerateButton);
        shoot.text = UIHelpers.KeycodeToChar(settings.FireButton);

        screenLayout.value = screenLayout.options.FindIndex(f => f.text == GameSettingsData.LayoutNameByType[settings.ScreenLayout]);

        screenResolution.value = settings.ScreenResolution;

        difficulty.value = settings.Difficulty;
    }

    public void OnDefaultClick()
    {
        settings.SetDefault();

        ReadValuesFromSettings();
    }


    public void OnBackClick()
    {
        gameObject.SetActive(false);
        mainOverlay.SetActive(true);
    }

    public void OnRotateCWClick()
    {

    }

    public void OnRotateCCWClick()
    {

    }

    public void OnAccelerateClick()
    {

    }

    public void OnShootClick()
    {

    }

    public void OnScreenLayoutChange()
    {
        var layout = GameSettingsData.LayoutTypeByName[screenLayout.options[screenLayout.value].text];
        Screen.SetResolution(Screen.width, Screen.height, layout);
    }

    public void OnScreenResolutionChange()
    {
        var resolution = GameSettingsData.ScreenResolutions[screenResolution.value];
        Screen.SetResolution(resolution.Item1, resolution.Item2, Screen.fullScreenMode);
    }

    public void OnDifficultyChange()
    {
        settings.Difficulty = (int)difficulty.value;
    }
}