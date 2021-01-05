using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int AlienShipScore;
    public int AsteroidScore;

    public static ScoreEvent ScoreEvent;

    public LevelManager LevelManager;
    public ScoreDisplay ScoreDisplay;

    private int score;

    private void Awake()
    {
        if (ScoreEvent == null)
        {
            ScoreEvent = new ScoreEvent();
        }
    }

    public void Initialize()
    {
        score = 0;
        UpdateScoreDisplay();

        ScoreEvent.AddListener(OnScore);
    }

    private void OnScore(string enemyTag, string playerTag)
    {
        int addedScore = 0;
        switch (enemyTag)
        {
            case Tags.ASTEROID:
                addedScore = AsteroidScore* LevelManager.Level;
                break;
            case Tags.ALIEN_SHIP:
                addedScore = AlienShipScore * LevelManager.Level;
                break;
        }

        if (playerTag == Tags.PLAYER)
        {
            addedScore /= 2;
        }

        AddScore(addedScore);
    }

    public void AddScore(int addedScore)
    {
        score += addedScore;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        ScoreDisplay.UpdateDisplay(score);
    }

    public void Terminate()
    {
        ScoreEvent.RemoveAllListeners();
    }
}