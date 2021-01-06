using System;
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
        AnnouncingService.Initialize();

        ScoreManager.Initialize();
        PlayerShipManager.Initialize();

        LevelManager.Level = 0;

        LevelManager.InitializeNextLevel();

        PlayerShipManager.PlayerDeathEvent.AddListener(OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        AnnouncingService.AnnounceGameOver();

        AnnouncingService.GameOverMessageOverEvent.AddListener(OnGameOverMessageOver);
    }

    private void OnGameOverMessageOver()
    {
        AnnouncingService.GameOverMessageOverEvent.RemoveListener(OnGameOverMessageOver);

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