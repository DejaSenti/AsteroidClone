using UnityEngine;

public class ScoreManager : MonoBehaviour, IGameManager
{
    private const int SHARPSHOOTER_BONUS = 1000;
    private const int KAMIKAZE_BONUS = 700;
    private const int UNDERDOG_BONUS = 300;
    private const int SLEEPING_BEAUTY_BONUS = 1;
    private const int ALIEN_SHIP_SCORE = 200;
    private const int ASTEROID_SCORE = 100;

    public LevelManager LevelManager;
    public ScoreDisplay ScoreDisplay;

    public int Score;

    public void Initialize()
    {
        Score = 0;
        UpdateScoreDisplay();

        AlienShipManager.AlienShipDestroyedEvent.AddListener(OnScoreableDestroyed);
        AsteroidManager.AsteroidDestroyedEvent.AddListener(OnScoreableDestroyed);
        LevelManager.EndLevelEvent.AddListener(OnEndLevel);
    }

    private void OnScoreableDestroyed(SpaceEntity scoreable, string destroyerTag)
    {
        if (destroyerTag == Tags.PLAYER || destroyerTag == Tags.PLAYER_GHOST || destroyerTag == Tags.PLAYER_BULLET || destroyerTag == Tags.PLAYER_BULLET_GHOST)
        {
            int addedScore = 0;

            switch (scoreable.tag)
            {
                case Tags.ASTEROID:
                    addedScore = ASTEROID_SCORE * LevelManager.Level;
                    break;
                case Tags.ASTEROID_GHOST:
                    addedScore = ASTEROID_SCORE * LevelManager.Level;
                    break;
                case Tags.ALIEN_SHIP:
                    addedScore = ALIEN_SHIP_SCORE * LevelManager.Level;
                    break;
                case Tags.ALIEN_SHIP_GHOST:
                    addedScore = ALIEN_SHIP_SCORE * LevelManager.Level;
                    break;
            }

            if (destroyerTag == Tags.PLAYER || destroyerTag == Tags.PLAYER_GHOST)
            {
                addedScore /= 2;
            }

            AddScore(addedScore);
        }
    }

    private void OnEndLevel(string destroyerTag)
    {
        switch (destroyerTag)
        {
            case Tags.PLAYER_BULLET:
                AddScore(SHARPSHOOTER_BONUS);
                break;
            case Tags.PLAYER_BULLET_GHOST:
                AddScore(SHARPSHOOTER_BONUS);
                break;
            case Tags.PLAYER:
                AddScore(KAMIKAZE_BONUS);
                break;
            case Tags.PLAYER_GHOST:
                AddScore(KAMIKAZE_BONUS);
                break;
            case Tags.ALIEN_SHIP_BULLET:
                AddScore(UNDERDOG_BONUS);
                break;
            case Tags.ALIEN_SHIP_BULLET_GHOST:
                AddScore(UNDERDOG_BONUS);
                break;
            case Tags.ALIEN_SHIP:
                AddScore(SLEEPING_BEAUTY_BONUS);
                break;
            case Tags.ALIEN_SHIP_GHOST:
                AddScore(SLEEPING_BEAUTY_BONUS);
                break;
        }
    }

    public void AddScore(int addedScore)
    {
        Score += addedScore;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        ScoreDisplay.UpdateDisplay(Score);
    }

    public void Terminate()
    {
        AlienShipManager.AlienShipDestroyedEvent.RemoveListener(OnScoreableDestroyed);
        AsteroidManager.AsteroidDestroyedEvent.RemoveListener(OnScoreableDestroyed);
        LevelManager.EndLevelEvent.RemoveListener(OnEndLevel);

        TerminateSubordinates();
    }

    public void TerminateSubordinates()
    {
        ScoreDisplay.Clear();
    }
}