using UnityEngine;

public class ScoreManager : MonoBehaviour, IGameManager
{
    public int AlienShipScore;
    public int AsteroidScore;

    public LevelManager LevelManager;
    public ScoreDisplay ScoreDisplay;

    private int score;

    public void Initialize()
    {
        score = 0;
        UpdateScoreDisplay();

        AlienShipManager.AlienShipDestroyedEvent.AddListener(OnScoreableDestroyed);
        AsteroidManager.AsteroidDestroyedEvent.AddListener(OnScoreableDestroyed);
    }

    private void OnScoreableDestroyed(SpaceEntity scoreable, string destroyerTag)
    {
        if (destroyerTag == Tags.PLAYER || destroyerTag == Tags.PLAYER_BULLET)
        {
            int addedScore = 0;

            switch (scoreable.tag)
            {
                case Tags.ASTEROID:
                    addedScore = AsteroidScore * LevelManager.Level;
                    break;
                case Tags.ALIEN_SHIP:
                    addedScore = AlienShipScore * LevelManager.Level;
                    break;
            }

            if (destroyerTag == Tags.PLAYER)
            {
                addedScore /= 2;
            }

            AddScore(addedScore);
        }
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
        AlienShipManager.AlienShipDestroyedEvent.RemoveListener(OnScoreableDestroyed);
        AsteroidManager.AsteroidDestroyedEvent.RemoveListener(OnScoreableDestroyed);
    }

    public void TerminateSubordinates()
    {
    }
}