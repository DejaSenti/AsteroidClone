using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string GAME_OVER_MESSAGE = "Game Over";

    public PlayerShipManager PlayerShipManager;
    public AsteroidManager AsteroidManager;

    public TextMeshProUGUI Announcements;

    public int Level;

    private void Start()
    {
        StartNewGame();
    }

    private void StartNewGame()
    {
        Announcements.text = "";

        ScoreManager.Instance.Initialize();

        Level = 1;
        AsteroidManager.Initialize(Level);

        PlayerShipManager.Initialize();
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
        StartNewGame();
    }
}
