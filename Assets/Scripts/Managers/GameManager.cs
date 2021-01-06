using UnityEngine;

[RequireComponent(typeof(Timer))]
public class GameManager : MonoBehaviour, IGameManager
{
    public PlayerShipManager PlayerShipManager;
    public ScoreManager ScoreManager;
    public LevelManager LevelManager;
    public AnnouncingService AnnouncingService;

    private void Start()
    {
        StartNewGame();
    }

    private void StartNewGame()
    {
        ScoreManager.Initialize();
        PlayerShipManager.Initialize();

        LevelManager.Level = 0;

        LevelManager.InitializeNextLevel();

        PlayerShipManager.PlayerDeathEvent.AddListener(OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        AnnouncingService.AnnounceGameOver();
        AnnouncingService.AnnouncementDisplayTimer.TimerElapsedEvent.AddListener(OnGameOverAnnouncementOver);
    }

    private void OnGameOverAnnouncementOver()
    {
        AnnouncingService.AnnouncementDisplayTimer.TimerElapsedEvent.RemoveListener(OnGameOverAnnouncementOver);

        Terminate();
    }

    [ContextMenu("Restart Game")]
    public void RestartGame()
    {
        Terminate();
        StartNewGame();
    }

    public void Terminate()
    {
        PlayerShipManager.PlayerDeathEvent.RemoveAllListeners();
        AnnouncingService.AnnouncementDisplayTimer.TimerElapsedEvent.RemoveAllListeners();

        TerminateSubordinates();
    }

    public void TerminateSubordinates()
    {
        PlayerShipManager.Terminate();
        ScoreManager.Terminate();
        LevelManager.Terminate();
        AnnouncingService.Terminate();
    }
}