using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : BaseMenu
{
#pragma warning disable 0649
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button instructionsButton;
    [SerializeField]
    private Button settingsButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private GameObject instructionsOverlay;
    [SerializeField]
    private GameObject settingsOverlay;
#pragma warning restore 0649

    public override void AddButtonListeners()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
        instructionsButton.onClick.AddListener(OnInstructionsButtonClick);
        settingsButton.onClick.AddListener(OnSettingsButtonClick);
        exitButton.onClick.AddListener(OnExitButtonClick);
    }

    public override void RemoveButtonListeners()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClick);
        instructionsButton.onClick.RemoveListener(OnInstructionsButtonClick);
        settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
        exitButton.onClick.RemoveListener(OnExitButtonClick);
    }

    private void OnPlayButtonClick()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }

    private void OnInstructionsButtonClick()
    {
        gameObject.SetActive(false);
        instructionsOverlay.SetActive(true);
    }

    private void OnSettingsButtonClick()
    {
        gameObject.SetActive(false);
        settingsOverlay.SetActive(true);
    }

    private void OnExitButtonClick()
    {
        RemoveButtonListeners();
        Application.Quit();
    }
}