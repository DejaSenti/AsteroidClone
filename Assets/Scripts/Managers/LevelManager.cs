using UnityEngine;

[RequireComponent(typeof(Timer))]
public class LevelManager : MonoBehaviour, IGameManager
{
    public AsteroidManager AsteroidManager;
    public AlienShipManager AlienShipManager;

    public AnnouncingService AnnouncingService;

    public static int Level;

    public void InitializeNextLevel()
    {
        Level++;

        AnnouncingService.AnnounceLevel(Level);
        AnnouncingService.AnnouncementDisplayTimer.TimerElapsedEvent.AddListener(OnLevelAnnouncementOver);

        InitializeLevel();
    }

    private void OnLevelAnnouncementOver()
    {
        AnnouncingService.AnnouncementDisplayTimer.TimerElapsedEvent.RemoveListener(OnLevelAnnouncementOver);

        StartLevel();
    }

    private void InitializeLevel()
    {
        AsteroidManager.Initialize();
        AlienShipManager.Initialize();
    }

    public void StartLevel()
    {
        AsteroidManager.StartLevel();
        AsteroidManager.AsteroidsClearedEvent.AddListener(OnAsteroidsDestroyed);

        AlienShipManager.StartLevel();
    }

    private void OnAsteroidsDestroyed()
    {
        AsteroidManager.AsteroidsClearedEvent.RemoveListener(OnAsteroidsDestroyed);

        TerminateSubordinates();

        InitializeNextLevel();
    }

    public void Terminate()
    {
        AsteroidManager.AsteroidsClearedEvent.RemoveAllListeners();
        AnnouncingService.AnnouncementDisplayTimer.TimerElapsedEvent.RemoveListener(OnLevelAnnouncementOver);

        TerminateSubordinates();
    }

    public void TerminateSubordinates()
    {
        AsteroidManager.Terminate();
        AlienShipManager.Terminate();
    }
}
