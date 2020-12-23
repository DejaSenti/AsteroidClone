using UnityEngine;
using ExtensionMethods;

public class AsteroidManager : MonoBehaviour
{
    private const int MAX_ASTEROID_SIZE = 3;
    private const int NUM_SPLIT_ASTEROIDS = 2;

    public int Level;

    private ObjectPool<Asteroid> asteroidPool;

    private AsteroidCollisionEvent asteroidCollisionEvent = new AsteroidCollisionEvent();

    void Awake()
    {
        asteroidPool = new ObjectPool<Asteroid>();
        int poolSize = Mathf.CeilToInt(Mathf.Pow(NUM_SPLIT_ASTEROIDS, MAX_ASTEROID_SIZE));
        asteroidPool.Initialize(poolSize, transform);

        asteroidCollisionEvent.AddListener(OnAsteroidCollisionEvent);
    }

    private void Start()
    {
        SpawnAsteroids(Level, MAX_ASTEROID_SIZE, Vector2.zero);
    }

    private void OnAsteroidCollisionEvent(Asteroid asteroid, string colliderTag)
    {
        asteroidPool.Kill(asteroid);

        if (asteroid.Size > 0)
        {
            SpawnAsteroids(NUM_SPLIT_ASTEROIDS, asteroid.Size - 1, asteroid.RB.position);
        }
    }

    private void SpawnAsteroids(int amount, int size, Vector2 location)
    {
        var positionArray = this.GetPositionArrayAroundLocation(amount, size, location);

        for (int i = 0; i < amount; i++)
        {
            var asteroid = asteroidPool.Spawn();
            asteroid.Size = size;
            asteroid.RB.position = positionArray[i];

            var asteroidSpeed = this.GetRandomInRange(asteroid.MinSpeed, asteroid.MaxSpeed);
            var asteroidDirection = this.GetDirectionInRangeRelatedToPosition(amount, location, asteroid.RB.position);

            asteroid.RB.velocity = asteroidSpeed * asteroidDirection;
        }
    }
}
