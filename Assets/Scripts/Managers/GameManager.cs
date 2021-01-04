using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Timer))]
public class GameManager : MonoBehaviour, IGameManager
{
    private const string GAME_START_MESSAGE = "Go!";
    private const string GAME_OVER_MESSAGE = "Game Over";
    private const float ANNOUNCEMENT_DISPLAY_TIME = 2;

    public PlayerShipManager PlayerShipManager;
    public ScoreManager ScoreManager;
    public LevelManager LevelManager;

    public TextMeshProUGUI Announcements;

    public Timer AnnouncementDisplayTimer;

    private void Start()
    {
        StartNewGame();
    }

    public void Initialize()
    {
        LevelManager.Initialize();
        ScoreManager.Initialize();
        PlayerShipManager.Initialize();
    }

    private void StartNewGame()
    {
        Announce(GAME_START_MESSAGE);

        LevelManager.Level = 1;

        Initialize();
        PlayerShipManager.PlayerDeathEvent.AddListener(OnPlayerDeath);
        LevelManager.AsteroidManager.AsteroidsClearedEvent.AddListener(OnAsteroidsCleared);
    }

    private void OnAsteroidsCleared()
    {
        Announce("Level " + LevelManager.Level);
    }

    private void Announce(string announcement)
    {
        Announcements.text = announcement;
        AnnouncementDisplayTimer.StartTimer(ANNOUNCEMENT_DISPLAY_TIME);
        AnnouncementDisplayTimer.TimerElapsedEvent.AddListener(OnAnnouncementDisplayTimerElapsed);
    }

    private void OnAnnouncementDisplayTimerElapsed()
    {
        Announcements.text = "";
        AnnouncementDisplayTimer.TimerElapsedEvent.RemoveListener(OnAnnouncementDisplayTimerElapsed);
    }

    private void OnPlayerDeath()
    {
        Terminate();
        Announce(GAME_OVER_MESSAGE);
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
        AnnouncementDisplayTimer.TimerElapsedEvent.RemoveAllListeners();

        TerminateSubordinates();
    }

    public void TerminateSubordinates()
    {
        PlayerShipManager.Terminate();
        ScoreManager.Terminate();
        LevelManager.Terminate();
    }
}