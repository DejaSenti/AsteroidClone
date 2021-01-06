using UnityEngine;

[RequireComponent(typeof(Timer))]
public class LevelManager : MonoBehaviour, IGameManager
{
    public AsteroidManager AsteroidManager;
    public AlienShipManager AlienShipManager;

    public AnnouncingService AnnouncingService;

    public static EndLevelEvent EndLevelEvent;

    public static int Level;

#pragma warning disable 0649
    [SerializeField]
    private PlayerShipManager playerShipManager;
#pragma warning restore 0649

    private void Awake()
    {
        if (EndLevelEvent == null)
        {
            EndLevelEvent = new EndLevelEvent();
        }
    }

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
        AsteroidManager.AsteroidsClearedEvent.AddListener(OnAsteroidsCleared);

        AlienShipManager.StartLevel();
    }

    private void OnAsteroidsCleared(string destroyerTag)
    {
        AsteroidManager.AsteroidsClearedEvent.RemoveListener(OnAsteroidsCleared);

        EndLevelEvent.Invoke(destroyerTag);

        TerminateSubordinates();

        if (playerShipManager.PlayerHealth > 1)
            InitializeNextLevel();
    }

    public void Terminate()
    {
        AnnouncingService.LevelMessageOverEvent.RemoveListener(OnLevelMessageOver);
        AsteroidManager.AsteroidsClearedEvent.RemoveListener(OnAsteroidsCleared);

        TerminateSubordinates();
    }

    public void TerminateSubordinates()
    {
        AsteroidManager.Terminate();
        AlienShipManager.Terminate();
    }
}
