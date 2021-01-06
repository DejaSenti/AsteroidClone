using UnityEngine;
using ExtensionMethods;

public class AsteroidManager : MonoBehaviour, IGameManager
{
    private const int FULL_CIRCLE_DEG = 360;

    public static EntityDestroyedEvent AsteroidDestroyedEvent;

    public AsteroidsClearedEvent AsteroidsClearedEvent;

    private ObjectPool<Asteroid> asteroidPool;

    void Awake()
    {
        if (AsteroidsClearedEvent == null)
        {
            AsteroidsClearedEvent = new AsteroidsClearedEvent();
        }

        if (AsteroidDestroyedEvent == null)
        {
            AsteroidDestroyedEvent = new EntityDestroyedEvent();
        }

        if (asteroidPool == null)
        {
            asteroidPool = new ObjectPool<Asteroid>();
        }
    }

    public void Initialize()
    {
        int poolSize = LevelManager.Level * Mathf.CeilToInt(Mathf.Pow(AsteroidData.NUM_SPLIT_ASTEROIDS, AsteroidData.MAX_ASTEROID_SIZE));
        asteroidPool.Initialize(poolSize);
    }

    public void StartLevel()
    {
        SpawnAsteroids(LevelManager.Level, AsteroidData.MAX_ASTEROID_SIZE, SpaceBoundary.Width / 3 * Vector2.right, Vector2.zero);
    }

    private void SpawnAsteroids(int amount, int size, Vector2 spawnCenter, Vector2 repulsionOrigin)
    {
        if (amount <= 0)
            return;

        Vector2[] semiCirclePositions = GetCircle(amount, size, spawnCenter);

        for (int i = 0; i < amount; i++)
        {
            var asteroid = asteroidPool.Acquire();

            if (asteroid == null)
                return;

            var asteroidDirection = GetDirection(amount, semiCirclePositions[i], spawnCenter);
            var asteroidRepulsionDirection = (semiCirclePositions[i] - repulsionOrigin).normalized;
            var asteroidSpeed = this.GetRandomInRange(asteroid.MinSpeed, asteroid.MaxSpeed);

            var asteroidVelocity = asteroidSpeed * (asteroidDirection + asteroidRepulsionDirection).normalized;

            asteroid.Initialize(size, semiCirclePositions[i], asteroidVelocity);

            asteroid.AsteroidCollisionEvent.AddListener(OnAsteroidCollision);
        }
    }

    private void OnAsteroidCollision(Asteroid asteroid, Collider2D collision)
    {
        if (collision.tag == Tags.ASTEROID || collision.tag == Tags.ASTEROID_GHOST)
            return;

        AsteroidDestroyedEvent.Invoke(asteroid, collision.tag);

        asteroid.Terminate();

        asteroidPool.Release(asteroid);

        if (asteroid.Size > 1)
        {
            int newSize = asteroid.Size - 1;
            SpawnAsteroids(AsteroidData.NUM_SPLIT_ASTEROIDS, newSize, asteroid.Position, collision.transform.position);
        }

        if (GetActiveAsteroids() == 0)
        {
            AsteroidsClearedEvent.Invoke(collision.tag);
        }
    }

    public int GetActiveAsteroids()
    {
        int result = asteroidPool.ActiveCount;
        return result;
    }

    public void Terminate()
    {
        TerminateSubordinates();
    }

    public void TerminateSubordinates()
    {
        var asteroids = asteroidPool.GetAllPooledObjects();

        foreach (Asteroid asteroid in asteroids)
        {
            asteroid.Terminate();
            asteroidPool.Release(asteroid);
        }

        asteroidPool.Terminate();
    }

    private Vector2[] GetCircle(int amount, int size, Vector2 position)
    {
        if (amount == 0)
            return null;

        Vector2[] result = new Vector2[amount];

        if (amount == 1)
        {
            result[0] = position;
            return result;
        }

        float sliceAngle = FULL_CIRCLE_DEG / amount;
        float radius = Mathf.Pow(2, size);

        Vector2 initialDirection = this.GetRandomDirection();

        for (int i = 0; i < amount; i++)
        {
            Vector2 direction = this.RotateVectorByDeg(initialDirection, (i + 0.5f) * sliceAngle);
            Vector2 newPosition = position + direction * radius;

            result[i] = newPosition;
        }

        return result;
    }

    private Vector2 GetDirection(int totalAmount, Vector2 position, Vector2 origin)
    {
        float sliceAngle = FULL_CIRCLE_DEG / totalAmount;
        Vector2 initialDirection = (position - origin).normalized;
        float rotationAngle = this.GetRandomInRange(-sliceAngle / 2, sliceAngle / 2);

        var result = this.RotateVectorByDeg(initialDirection, rotationAngle);
        return result;
    }
}