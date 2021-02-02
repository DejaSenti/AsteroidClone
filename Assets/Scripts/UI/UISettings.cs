using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISettings : BaseMenu
{
#pragma warning disable 0649
    [SerializeField]
    private Button saveButton;
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private GameObject mainOverlay;

    [SerializeField]
    private Button rotateCW;
    [SerializeField]
    private Button rotateCCW;
    [SerializeField]
    private Button accelerate;
    [SerializeField]
    private Button shoot;

    [SerializeField]
    private TMP_Dropdown screenLayout;
    [SerializeField]
    private TMP_Dropdown screenResolution;
    [SerializeField]
    private TMP_InputField screenWidth;
    [SerializeField]
    private TMP_InputField screenHeight;

    [SerializeField]
    private Slider difficulty;
#pragma warning restore 0649

    public override void AddButtonListeners()
    {
        saveButton.onClick.AddListener(OnSaveButtonClick);
        backButton.onClick.AddListener(OnBackButtonClick);
    }

    public override void RemoveButtonListeners()
    {
        saveButton.onClick.RemoveListener(OnSaveButtonClick);
        backButton.onClick.RemoveListener(OnBackButtonClick);
    }

    private void OnSaveButtonClick()
    {
        throw new NotImplementedException();
    }

    private void OnBackButtonClick()
    {
        gameObject.SetActive(false);
        mainOverlay.SetActive(true);
    }
}