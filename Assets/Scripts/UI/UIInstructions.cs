using TMPro;
using UnityEngine;

public class UIInstructions : MonoBehaviour
{
    private const string INTRUCTIONS_TEXT_FORMAT = "• Use {0} and {1} to rotate clockwise and counter-clockwise\n" +
        "• Use {2} to accelerate forward\n" +
        "• Use {3} button to fire your gun\n\n" +
        "• Clear all asteroids to finish a level!\n" +
        "• Receive a bonus for completing a level in a particular way!\n" +
        "• Alien Ships will appear and try to disrupt your game!";

#pragma warning disable 0649
    [SerializeField]
    private GameObject mainOverlay;
    [SerializeField]
    private TMP_Text instructionText;
#pragma warning restore 0649

    private void Start()
    {
        var leftButtonChar = UIHelpers.KeycodeToText(MainMenuEntry.Settings.LeftButton);
        var rightButtonChar = UIHelpers.KeycodeToText(MainMenuEntry.Settings.RightButton);
        var accelerateButtonChar = UIHelpers.KeycodeToText(MainMenuEntry.Settings.AccelerateButton);
        var fireButtonChar = UIHelpers.KeycodeToText(MainMenuEntry.Settings.FireButton);

        var instructions = string.Format(INTRUCTIONS_TEXT_FORMAT, leftButtonChar, rightButtonChar, accelerateButtonChar, fireButtonChar);

        instructionText.text = instructions;
    }

    public void OnBackButtonClick()
    {
        gameObject.SetActive(false);
        mainOverlay.SetActive(true);
    }
}
