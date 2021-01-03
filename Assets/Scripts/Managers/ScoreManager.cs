using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour, IGameManager
{
    public static ScoreManager Instance;

    private void Awake()
    {
        Instance = GetComponent<ScoreManager>();
    }

    public TextMeshProUGUI ScoreDisplay;

    private int score;
    
    public void Initialize()
    {
        score = 0;
        UpdateDisplay();
    }

    public void AddScore(int addedScore)
    {
        score += addedScore;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        ScoreDisplay.text = score.ToString();
    }

    public void Terminate()
    {
        // remove listeners etc.
    }
}