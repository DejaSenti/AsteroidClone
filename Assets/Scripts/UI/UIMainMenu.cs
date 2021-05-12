using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    private GameObject instructionsOverlay;
    [SerializeField]
    private GameObject settingsOverlay;
#pragma warning restore 0649

    public void OnPlayButtonClick()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }

    public void OnInstructionsButtonClick()
    {
        gameObject.SetActive(false);
        instructionsOverlay.SetActive(true);
    }

    public void OnSettingsButtonClick()
    {
        gameObject.SetActive(false);
        settingsOverlay.SetActive(true);
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}