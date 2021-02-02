using System;
using UnityEngine;
using UnityEngine.UI;

public class UIInstructions : BaseMenu
{
#pragma warning disable 0649
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private GameObject mainOverlay;
#pragma warning restore 0649

    public override void AddButtonListeners()
    {
        backButton.onClick.AddListener(OnBackButtonClick);
    }

    public override void RemoveButtonListeners()
    {
        backButton.onClick.RemoveListener(OnBackButtonClick);
    }

    private void OnBackButtonClick()
    {
        gameObject.SetActive(false);
        mainOverlay.SetActive(true);
    }
}
