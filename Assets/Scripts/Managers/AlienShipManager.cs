using UnityEngine;
using ExtensionMethods;

[RequireComponent(typeof(Timer))]
public class AlienShipManager : MonoBehaviour, IGameManager
{
    private const float SPAWN_DELAY_MULTIPLIER = 30f;

    public Timer SpawnDelayTimer;

    private ObjectPool<AlienShip> alienShipPool;

    private void Awake()
    {
        if (alienShipPool == null)
        {
            alienShipPool = new ObjectPool<AlienShip>();
        }
    }

    public void Initialize()
    {
        alienShipPool.Initialize(LevelManager.Level);
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
        float result = this.GetRandomInRange(0, SPAWN_DELAY_MULTIPLIER / LevelManager.Level);
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
        var alienShip = alienShipPool.Acquire();

        if (alienShip == null)
            return;

        Vector2 position = new Vector2(SpaceBoundary.Width / 2, this.GetRandomInRange(-SpaceBoundary.Height / 2, SpaceBoundary.Height / 2));

        float speed = this.GetRandomInRange(alienShip.MinSpeed, alienShip.MaxSpeed);
        Vector2 direction = this.GetRandomDirection();
        Vector2 velocity = speed * direction;

        alienShip.Initialize(position, velocity);

        alienShip.AlienShipCollisionEvent.AddListener(OnAlienShipCollision);
    }

    private void OnAlienShipCollision(AlienShip alienShip, Collider2D collision)
    {
        if (collision.tag == Tags.PLAYER || collision.tag == Tags.PLAYER_BULLET)
        {
            ScoreManager.ScoreEvent.Invoke(Tags.ALIEN_SHIP, collision.tag);
        }

        alienShip.AlienShipCollisionEvent.RemoveListener(OnAlienShipCollision);

        alienShipPool.Release(alienShip);

        StartSpawnDelay();
    }

    public void Terminate()
    {
        SpawnDelayTimer.TimerElapsedEvent.RemoveAllListeners();
        TerminateSubordinates();
    }

    public void TerminateSubordinates()
    {
        var alienShips = alienShipPool.GetAllPooledObjects();

        foreach (AlienShip alienShip in alienShips)
        {
            alienShip.Terminate();
            alienShipPool.Release(alienShip);
            alienShip.AlienShipCollisionEvent.RemoveAllListeners();
        }

        alienShipPool.Terminate();
    }
}