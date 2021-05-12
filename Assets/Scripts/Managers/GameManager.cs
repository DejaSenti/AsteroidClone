using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Timer))]
public class GameManager : MonoBehaviour, IGameManager
{
    public PlayerShipManager PlayerShipManager;
    public ScoreManager ScoreManager;
    public LevelManager LevelManager;
    public AnnouncingService AnnouncingService;
    public ExplosionParticleSystem ExplosionParticleSystem;
    public Animator BackgroundAnimation;

    public Button MenuButton;
    public Button ResumeButton;
    public Button ExitButton;

    public GameObject PauseOverlay;

    public static bool IsGamePaused;

    private bool isGameRunning;

    private KeyCode pauseButton = KeyCode.Escape;
    private bool isPauseDown { get => Input.GetKeyDown(pauseButton); }

    private void Start()
    {
        BackgroundAnimation.Play("BG Loop");

        isGameRunning = true;

        StartNewGame();
    }

    private void Update()
    {
        if (isGameRunning && isPauseDown && !AnnouncingService.IsDuringAnnouncement)
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

    private void ShowPause()
    {
        PauseOverlay.SetActive(true);
        Cursor.visible = true;
    }

    private void HidePause()
    {
        PauseOverlay.SetActive(false);
        Cursor.visible = false;
    }

    public void OnResumeClick()
    {
        UnpauseGame();
    }

    public void OnMenuClick()
    {
        TogglePause();

        Terminate();

        HidePause();

        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnExitClick()
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
        Cursor.visible = false;

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

        SceneManager.LoadScene("MainMenu");
    }

    public void Terminate()
    {
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