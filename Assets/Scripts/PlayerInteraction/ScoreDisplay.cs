using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI Display;

    public void UpdateDisplay(int score)
    {
        Display.text = score.ToString();
    }

    public void Clear()
    {
        Display.text = "";
    }
}