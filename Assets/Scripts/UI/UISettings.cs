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
    private KeySetting[] keySettings = new KeySetting[4];

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
            foreach (var option in GameSettingsData.LayoutNameByType)
            {
                var newOption = new OptionData(option.Value);
                screenLayoutOptions.Add(newOption);
            }

            screenLayout.options = screenLayoutOptions;
        }

        if (screenResolutionOptions.Count == 0)
        {
            foreach (var option in GameSettingsData.ScreenResolutions)
            {
                var text = UIHelpers.ResolutionTupleToText(option);
                var newOption = new OptionData(text);
                screenResolutionOptions.Add(newOption);
            }

            screenResolution.options = screenResolutionOptions;
        }
    }

    private void Start()
    {
        ReadValuesFromSettings();
        UpdateKeySettingsDisplay();
    }

    private void OnEnable()
    {
        foreach (var keySetting in keySettings)
        {
            keySetting.KeyChangedEvent.AddListener(UpdateKeySettingsDisplay);
        }
    }

    private void ReadValuesFromSettings()
    {
        screenLayout.value = screenLayout.options.FindIndex(f => f.text == GameSettingsData.LayoutNameByType[settings.ScreenLayout]);

        screenResolution.value = settings.ScreenResolution;

        difficulty.value = settings.Difficulty;
    }

    public void OnDefaultClick()
    {
        settings.SetDefault();

        ReadValuesFromSettings();

        foreach (var keySetting in keySettings)
        {
            keySetting.SetDefault();
            keySetting.UpdateDisplay();
        }
    }


    public void OnBackClick()
    {
        settings.SaveSettingsJson();

        gameObject.SetActive(false);
        mainOverlay.SetActive(true);
    }

    public void OnScreenLayoutChange()
    {
        var layout = GameSettingsData.LayoutTypeByName[screenLayout.options[screenLayout.value].text];
        settings.ApplyScreenLayout(layout);
    }

    public void OnScreenResolutionChange()
    {
        settings.ApplyResolution(screenResolution.value);
    }

    public void OnDifficultyChange()
    {
        settings.ApplyDifficulty((int)difficulty.value);
    }

    private void UpdateKeySettingsDisplay()
    {
        foreach (var keySetting in keySettings)
        {
            keySetting.UpdateDisplay();
        }
    }

    private void OnDisable()
    {
        foreach (var keySetting in keySettings)
        {
            keySetting.KeyChangedEvent.RemoveListener(UpdateKeySettingsDisplay);
        }
    }
}