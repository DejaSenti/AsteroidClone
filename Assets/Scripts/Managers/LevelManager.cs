using UnityEngine;

[RequireComponent(typeof(Timer))]
public class LevelManager : MonoBehaviour, IGameManager
{
    private const float LEVEL_DELAY = 2f;

    public AsteroidManager AsteroidManager;
    public AlienShipManager AlienShipManager;

    public Timer LevelDelayTimer;

    public int Level;

    public void Initialize(int level)
    {
        AsteroidManager.Initialize(level);
        AsteroidManager.AsteroidsClearedEvent.AddListener(OnAsteroidsDestroyed);

        AlienShipManager.Initialize(level);
    }

    private void OnAsteroidsDestroyed()
    {
        AsteroidManager.AsteroidsClearedEvent.RemoveListener(OnAsteroidsDestroyed);

        TerminateSubordinates();

        Level++;
        LevelDelayTimer.StartTimer(LEVEL_DELAY);
        LevelDelayTimer.TimerElapsedEvent.AddListener(OnLevelDelayElapsed);
    }

    private void OnLevelDelayElapsed()
    {
        LevelDelayTimer.TimerElapsedEvent.RemoveListener(OnLevelDelayElapsed);

        Initialize(Level);
    }

    public void Terminate()
    {
        LevelDelayTimer.TimerElapsedEvent.RemoveAllListeners();
        AsteroidManager.AsteroidsClearedEvent.RemoveAllListeners();

        TerminateSubordinates();
    }

    public void TerminateSubordinates()
    {
        AsteroidManager.Terminate();
        AlienShipManager.Terminate();
    }
}
