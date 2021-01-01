using UnityEngine;
using ExtensionMethods;

public class AsteroidManager : MonoBehaviour
{
    private ObjectPool<Asteroid> asteroidPool;

    void Awake()
    {
        asteroidPool = new ObjectPool<Asteroid>();
    }

    public void Initialize(int level)
    {
        int poolSize = level * Mathf.CeilToInt(Mathf.Pow(AsteroidData.NUM_SPLIT_ASTEROIDS, AsteroidData.MAX_ASTEROID_SIZE));
        asteroidPool.Initialize(poolSize, transform);

        SpawnAsteroids(level, AsteroidData.MAX_ASTEROID_SIZE, SpaceBoundary.Width / 3 * Vector2.right);
    }

    private void OnAsteroidCollisionEvent(Asteroid asteroid, Collider2D collision)
    {
        if (asteroid.Size > 1)
        {
            int newSize = asteroid.Size - 1;
            SpawnAsteroids(AsteroidData.NUM_SPLIT_ASTEROIDS, newSize, asteroid.Position);
        }

        asteroid.AsteroidCollisionEvent.RemoveListener(OnAsteroidCollisionEvent);

        asteroidPool.Release(asteroid);
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

            asteroid.AsteroidCollisionEvent.AddListener(OnAsteroidCollisionEvent);
        }
    }
}
