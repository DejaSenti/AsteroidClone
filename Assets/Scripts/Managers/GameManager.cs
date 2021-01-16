using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Timer))]
public class GameManager : MonoBehaviour, IGameManager
{
    public PlayerShipManager PlayerShipManager;
    public ScoreManager ScoreManager;
    public LevelManager LevelManager;
    public AnnouncingService AnnouncingService;
    public ExplosionParticleSystem ExplosionParticleSystem;

    public Button PlayButton;
    public Button ExitButton;

    public Button MenuButton;
    public Button ResumeButton;
    public Button PauseExitButton;

    public GameObject MenuOverlay;
    public GameObject PauseOverlay;

    public static bool IsGamePaused;

    private bool isGameRunning;

    private KeyCode pauseButton = KeyCode.Escape;
    private bool isPauseDown { get => Input.GetKeyDown(pauseButton); }

    private void Start()
    {
        ShowMenu();
    }

    private void Update()
    {
        if (isGameRunning && isPauseDown)
        {
            if (IsGamePaused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void ShowMenu()
    {
        MenuOverlay.SetActive(true);
        PlayButton.onClick.AddListener(OnPlayClick);
        ExitButton.onClick.AddListener(OnExitClick);
    }

    private void HideMenu()
    {
        MenuOverlay.SetActive(false);
        PlayButton.onClick.RemoveListener(OnPlayClick);
        ExitButton.onClick.RemoveListener(OnExitClick);
    }

    private void ShowPause()
    {
        PauseOverlay.SetActive(true);

        ResumeButton.onClick.AddListener(OnResumeClick);
        MenuButton.onClick.AddListener(OnMenuClick);
        PauseExitButton.onClick.AddListener(OnExitClick);
    }

    private void HidePause()
    {
        PauseOverlay.SetActive(false);

        ResumeButton.onClick.RemoveListener(OnResumeClick);
        MenuButton.onClick.RemoveListener(OnMenuClick);
        PauseExitButton.onClick.RemoveListener(OnExitClick);
    }

    private void OnPlayClick()
    {
        HideMenu();

        isGameRunning = true;

        StartNewGame();
    }

    private void OnResumeClick()
    {
        UnpauseGame();
    }

    private void OnMenuClick()
    {
        TogglePause();

        Terminate();

        HidePause();
        ShowMenu();
    }

    private void OnExitClick()
    {
        Terminate();
        Application.Quit();
    }

    private void PauseGame()
    {
        TogglePause();
        ShowPause();
    }

    private void UnpauseGame()
    {
        TogglePause();
        HidePause();
    }

    private void TogglePause()
    {
        IsGamePaused = !IsGamePaused;
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }

    private void StartNewGame()
    {
        AnnouncingService.Initialize();

        ScoreManager.Initialize();
        PlayerShipManager.Initialize();

        LevelManager.Level = 0;

        LevelManager.InitializeNextLevel();

        ExplosionParticleSystem.Initialize();

        PlayerShipManager.PlayerDeathEvent.AddListener(OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        AnnouncingService.AnnounceGameOver(ScoreManager.Score);

        AnnouncingService.GameOverMessageOverEvent.AddListener(OnGameOverMessageOver);
    }

    private void OnGameOverMessageOver()
    {
        AnnouncingService.GameOverMessageOverEvent.RemoveListener(OnGameOverMessageOver);

        Terminate();
        ShowMenu();
    }

    public void Terminate()
    {
        PlayButton.onClick.RemoveListener(OnPlayClick);
        ExitButton.onClick.RemoveListener(OnExitClick);

        AnnouncingService.GameOverMessageOverEvent.RemoveListener(OnGameOverMessageOver);
        TerminateSubordinates();
    }

    public void TerminateSubordinates()
    {
        ExplosionParticleSystem.Terminate();
        PlayerShipManager.Terminate();
        ScoreManager.Terminate();
        LevelManager.Terminate();
        AnnouncingService.Terminate();
    }
}