using UnityEngine;
using ExtensionMethods;

[RequireComponent(typeof(Timer))]
public class AlienShipManager : MonoBehaviour
{
    private const float SPAWN_DELAY_MULTIPLIER = 30f;

    public Timer spawnDelayTimer;

    private ObjectPool<AlienShip> alienShipPool;

    private int level;

    private void Start()
    {
        if (alienShipPool == null)
        {
            alienShipPool = new ObjectPool<AlienShip>();
        }
    }

    public void Initialize(int level)
    {
        this.level = level;
        alienShipPool.Initialize(level);

        StartSpawnDelay();
    }

    private void StartSpawnDelay()
    {
        var spawnDelay = GetShipSpawnDelay();
        spawnDelayTimer.StartTimer(spawnDelay);
        spawnDelayTimer.TimerElapsedEvent.AddListener(OnSpawnDelayElapsed);
    }

    private float GetShipSpawnDelay()
    {
        float result = this.GetRandomInRange(0, SPAWN_DELAY_MULTIPLIER / level);
        return result;
    }

    private void OnSpawnDelayElapsed()
    {
        spawnDelayTimer.TimerElapsedEvent.RemoveListener(OnSpawnDelayElapsed);

        SpawnAlienShip();

        StartSpawnDelay();
    }

    private void SpawnAlienShip()
    {
        var alienShip = alienShipPool.Acquire();

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
        spawnDelayTimer.TimerElapsedEvent.RemoveAllListeners();
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