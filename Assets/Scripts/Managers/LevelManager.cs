using UnityEngine;

[RequireComponent(typeof(Timer))]
public class LevelManager : AnnouncingManager, IGameManager
{
    public AsteroidManager AsteroidManager;
    public AlienShipManager AlienShipManager;

    public static int Level;

    public void InitializeLevel()
    {
        AsteroidManager.Initialize();
        AlienShipManager.Initialize();
    }

    public void StartNextLevel()
    {
        Level++;

        string levelAnnouncement = "Level " + LevelManager.Level;

        Announce(levelAnnouncement);

        InitializeLevel();
    }

    public override void OnAnnouncementDisplayTimerElapsed()
    {
        base.OnAnnouncementDisplayTimerElapsed();

        StartLevel();
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

        StartNextLevel();
    }

    public override void Terminate()
    {
        base.Terminate();

        AsteroidManager.AsteroidsClearedEvent.RemoveAllListeners();

        TerminateSubordinates();
    }

    public void TerminateSubordinates()
    {
        AsteroidManager.Terminate();
        AlienShipManager.Terminate();
    }
}
