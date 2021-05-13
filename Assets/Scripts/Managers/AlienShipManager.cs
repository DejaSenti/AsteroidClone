using UnityEngine;
using ExtensionMethods;

[RequireComponent(typeof(Timer))]
public class AlienShipManager : MonoBehaviour, IGameManager
{
    private const float SPAWN_DELAY_MULTIPLIER = 30f;

    public static EntityDestroyedEvent AlienShipDestroyedEvent;

    public Timer SpawnDelayTimer;

    private ObjectPool<AlienShip> alienShipPool;

    private void Awake()
    {
        if (alienShipPool == null)
        {
            alienShipPool = new ObjectPool<AlienShip>();
        }

        if (AlienShipDestroyedEvent == null)
        {
            AlienShipDestroyedEvent = new EntityDestroyedEvent();
        }
    }

    public void Initialize()
    {
        alienShipPool.Initialize(LevelManager.Level);

        foreach(AlienShip alienShip in alienShipPool.GetAllPooledObjects())
        {
            alienShip.Initialize(Vector2.zero, Vector2.zero);
        }
    }

    public void StartLevel()
    {
        StartSpawnDelay();
    }

    private void StartSpawnDelay()
    {
        var spawnDelay = GetShipSpawnDelay();
        SpawnDelayTimer.StartTimer(spawnDelay);
        SpawnDelayTimer.TimerElapsedEvent.AddListener(OnSpawnDelayElapsed);
    }

    private float GetShipSpawnDelay()
    {
        float result = this.GetRandomInRange(SPAWN_DELAY_MULTIPLIER / LevelManager.Level / 2, SPAWN_DELAY_MULTIPLIER / LevelManager.Level) / (MainMenuEntry.Settings.Difficulty + 1);
        return result;
    }

    private void OnSpawnDelayElapsed()
    {
        SpawnDelayTimer.TimerElapsedEvent.RemoveListener(OnSpawnDelayElapsed);

        SpawnAlienShip();

        StartSpawnDelay();
    }

    private void SpawnAlienShip()
    {
        Vector2? position_ = SpawnPointManager.Instance.GetAlienShipSpawnPoint();
        Vector2 position;

        if (position_ == null)
            return;

        var alienShip = alienShipPool.Acquire();

        if (alienShip == null)
            return;

        position = (Vector2)position_;

        float speed = this.GetRandomInRange(alienShip.MinSpeed, alienShip.MaxSpeed);
        Vector2 direction = (Vector2.zero - position).normalized;
        Vector2 velocity = speed * direction;

        alienShip.Initialize(position, velocity);

        alienShip.AlienShipCollisionEvent.AddListener(OnAlienShipCollision);
    }

    private void OnAlienShipCollision(AlienShip alienShip, Collider2D collision)
    {
        AlienShipDestroyedEvent.Invoke(alienShip, collision.tag);

        alienShip.Terminate();

        alienShipPool.Release(alienShip);
    }

    public void Terminate()
    {
        SpawnDelayTimer.ResetTimer();
        SpawnDelayTimer.TimerElapsedEvent.RemoveListener(OnSpawnDelayElapsed);

        TerminateSubordinates();
    }

    public void TerminateSubordinates()
    {
        var alienShips = alienShipPool.GetAllPooledObjects();

        foreach (AlienShip alienShip in alienShips)
        {
            alienShip.Terminate();
            alienShipPool.Release(alienShip);
        }

        alienShipPool.Terminate();
    }
}
