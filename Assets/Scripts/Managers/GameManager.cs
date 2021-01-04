using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    private const string GAME_OVER_MESSAGE = "Game Over";

    public PlayerShipManager PlayerShipManager;
    public ScoreManager ScoreManager;
    public LevelManager LevelManager;

    public TextMeshProUGUI Announcements;

    public int Level;

    private void Start()
    {
        StartNewGame();
    }

    private void StartNewGame()
    {
        Announcements.text = "";

        Level = 1;

        Initialize(Level);
        PlayerShipManager.PlayerDeathEvent.AddListener(OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        Terminate();
        Announcements.text = GAME_OVER_MESSAGE;
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        Terminate();
        StartNewGame();
    }

    public void Initialize(int level)
    {
        LevelManager.Initialize(level);
        ScoreManager.Initialize();
        PlayerShipManager.Initialize();
    }

    public void Terminate()
    {
        PlayerShipManager.PlayerDeathEvent.RemoveAllListeners();

        TerminateSubordinates();
    }

    public void TerminateSubordinates()
    {
        PlayerShipManager.Terminate();
        ScoreManager.Terminate();
        LevelManager.Terminate();
    }
}