using TMPro;
using UnityEngine;

public class PlayerHealthDisplay : MonoBehaviour
{
    private const char HEALTH_LETTER = 'Y';

    public TextMeshProUGUI HealthDisplayText;

    public void UpdateDisplay(int playerHealth)
    {
        var playerHealthDisplay = new string(HEALTH_LETTER, playerHealth);
        HealthDisplayText.text = playerHealthDisplay;
    }

    public void Clear()
    {
        HealthDisplayText.text = "";
    }
}