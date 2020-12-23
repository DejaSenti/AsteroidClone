using UnityEngine;
using ExtensionMethods;

public class AsteroidManager : MonoBehaviour
{
    private const int MAX_ASTEROID_SIZE = 3;
    private const int NUM_SPLIT_ASTEROIDS = 3;

    public int Level;

    private ObjectPool<Asteroid> asteroidPool;

    void Awake()
    {
        asteroidPool = new ObjectPool<Asteroid>();
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        int poolSize = Level * Mathf.CeilToInt(Mathf.Pow(NUM_SPLIT_ASTEROIDS, MAX_ASTEROID_SIZE));
        asteroidPool.Initialize(poolSize, transform);

        SpawnAsteroids(Level, MAX_ASTEROID_SIZE, Vector2.zero);
    }

    private void OnAsteroidCollisionEvent(Asteroid asteroid, string colliderTag)
    {
        if (colliderTag == Tags.ASTEROID_TAG)
            return;

        if (asteroid.Size > 0)
        {
            int newSize = asteroid.Size - 1;
            SpawnAsteroids(NUM_SPLIT_ASTEROIDS, newSize, asteroid.Position);
        }

        asteroid.AsteroidCollisionEvent.RemoveListener(OnAsteroidCollisionEvent);

        asteroidPool.Kill(asteroid);
    }

    private void SpawnAsteroids(int amount, int size, Vector2 location)
    {
        var positionArray = this.GetPositionArrayAroundLocation(amount, size, location);

        for (int i = 0; i < amount; i++)
        {
            var asteroid = asteroidPool.Spawn();

            if (asteroid == null)
                continue;

            var asteroidSpeed = this.GetRandomInRange(asteroid.MinSpeed, asteroid.MaxSpeed);
            var asteroidDirection = this.GetDirectionInRangeRelatedToPosition(amount, location, asteroid.Position);
            var asteroidVelocity = asteroidSpeed * asteroidDirection;

            asteroid.Initialize(size, positionArray[i], asteroidVelocity);

            asteroid.AsteroidCollisionEvent.AddListener(OnAsteroidCollisionEvent);
        }
    }

    [ContextMenu("Restart Level")]
    public void RestartLevel()
    {
        Initialize();
    }
}
