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
        AnnouncingService.LevelMessageOverEvent.AddListener(OnLevelMessageOver);

        InitializeLevel();
    }

    private void OnLevelMessageOver()
    {
        AnnouncingService.LevelMessageOverEvent.RemoveListener(OnLevelMessageOver);

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

    private void OnAsteroidsDestroyed(string destroyerTag)
    {
        AsteroidManager.AsteroidsClearedEvent.RemoveListener(OnAsteroidsDestroyed);

        switch (destroyerTag)
        {
            case Tags.PLAYER_BULLET:
                AnnouncingService.AnnounceSharpshooter();
                break;
            case Tags.PLAYER:
                AnnouncingService.AnnounceKamikaze();
                break;
            case Tags.ALIEN_SHIP_BULLET:
                AnnouncingService.AnnounceUnderdog();
                break;
            case Tags.ALIEN_SHIP:
                AnnouncingService.AnnounceSleepingBeauty();
                break;
        }

        TerminateSubordinates();

        InitializeNextLevel();
    }

    public void Terminate()
    {
        TerminateSubordinates();
    }

    public void TerminateSubordinates()
    {
        AsteroidManager.Terminate();
        AlienShipManager.Terminate();
    }
}
