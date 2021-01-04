using UnityEngine;
using ExtensionMethods;

public class AsteroidManager : MonoBehaviour, IGameManager
{
    public AsteroidsClearedEvent AsteroidsClearedEvent;

    private ObjectPool<Asteroid> asteroidPool;

    void Awake()
    {
        if (AsteroidsClearedEvent == null)
        {
            AsteroidsClearedEvent = new AsteroidsClearedEvent();
        }

        if (asteroidPool == null)
        {
            asteroidPool = new ObjectPool<Asteroid>();
        }
    }

    public void Initialize(int level)
    {
        int poolSize = level * Mathf.CeilToInt(Mathf.Pow(AsteroidData.NUM_SPLIT_ASTEROIDS, AsteroidData.MAX_ASTEROID_SIZE));
        asteroidPool.Initialize(poolSize);

        SpawnAsteroids(level, AsteroidData.MAX_ASTEROID_SIZE, SpaceBoundary.Width / 3 * Vector2.right);
    }

    private void SpawnAsteroids(int amount, int size, Vector2 location)
    {
        for (int i = 0; i < amount; i++)
        {
            var asteroid = asteroidPool.Acquire();

            if (asteroid == null)
                return;

            var asteroidSpeed = this.GetRandomInRange(asteroid.MinSpeed, asteroid.MaxSpeed);
            var asteroidDirection = this.GetRandomDirection();
            var asteroidVelocity = asteroidSpeed * asteroidDirection;

            asteroid.Initialize(size, location, asteroidVelocity);

            asteroid.AsteroidCollisionEvent.AddListener(OnAsteroidCollision);
        }
    }

    private void OnAsteroidCollision(Asteroid asteroid, Collider2D collision)
    {
        if (collision.tag == Tags.PLAYER || collision.tag == Tags.PLAYER_BULLET)
        {
            ScoreManager.ScoreEvent.Invoke(Tags.ASTEROID, collision.tag);
        }

        asteroid.AsteroidCollisionEvent.RemoveListener(OnAsteroidCollision);

        asteroidPool.Release(asteroid);

        if (GetActiveAsteroids() == 0)
        {
            AsteroidsClearedEvent.Invoke();
        }

        if (asteroid.Size > 1)
        {
            int newSize = asteroid.Size - 1;
            SpawnAsteroids(AsteroidData.NUM_SPLIT_ASTEROIDS, newSize, asteroid.Position);
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
            asteroid.AsteroidCollisionEvent.RemoveAllListeners();
        }

        asteroidPool.Terminate();
    }
}