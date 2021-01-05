using UnityEngine;

[RequireComponent(typeof(Timer))]
public class GameManager : AnnouncingManager, IGameManager
{
    private const string GAME_OVER_MESSAGE = "Game Over";

    public PlayerShipManager PlayerShipManager;
    public ScoreManager ScoreManager;
    public LevelManager LevelManager;

    private void Start()
    {
        StartNewGame();
    }

    private void StartNewGame()
    {
        ScoreManager.Initialize();
        PlayerShipManager.Initialize();

        LevelManager.Level = 0;

        LevelManager.StartNextLevel();

        PlayerShipManager.PlayerDeathEvent.AddListener(OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        Announce(GAME_OVER_MESSAGE, Terminate);
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        Terminate();
        StartNewGame();
    }

    public override void Terminate()
    {
        base.Terminate();

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