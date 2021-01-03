using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour, IGameManager
{
    private const string GAME_OVER_MESSAGE = "Game Over";

    public PlayerShipManager PlayerShipManager;
    public AsteroidManager AsteroidManager;

    public TextMeshProUGUI Announcements;

    public int Level;

    private void Start()
    {
        Initialize();
        StartNewGame();
    }

    private void StartNewGame()
    {
        Announcements.text = "";

        Level = 1;

        PlayerShipManager.PlayerDeathEvent.AddListener(OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        PlayerShipManager.PlayerDeathEvent.RemoveListener(OnPlayerDeath);
        Announcements.text = GAME_OVER_MESSAGE;
    }

    [ContextMenu("Restart Level")]
    public void RestartLevel()
    {
        Terminate();
        StartNewGame();
    }

    public void Initialize()
    {
        ScoreManager.Instance.Initialize();
        AsteroidManager.Initialize();
        PlayerShipManager.Initialize();
    }

    public void Terminate()
    {
        PlayerShipManager.PlayerDeathEvent.RemoveAllListeners();
        PlayerShipManager.Terminate();
        AsteroidManager.Terminate();
        ScoreManager.Instance.Terminate();
    }
}
