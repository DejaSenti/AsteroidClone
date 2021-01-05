using UnityEngine;
using ExtensionMethods;

public class AsteroidManager : MonoBehaviour, IGameManager
{
    private const int SEMI_CIRCLE_DEG = 180;

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

    public void Initialize()
    {
        int poolSize = LevelManager.Level * Mathf.CeilToInt(Mathf.Pow(AsteroidData.NUM_SPLIT_ASTEROIDS, AsteroidData.MAX_ASTEROID_SIZE));
        asteroidPool.Initialize(poolSize);
    }

    public void StartLevel()
    {
        SpawnAsteroids(LevelManager.Level, AsteroidData.MAX_ASTEROID_SIZE, SpaceBoundary.Width / 3 * Vector2.right, Vector2.zero);
    }

    private void SpawnAsteroids(int amount, int size, Vector2 location, Vector2 origin)
    {
        if (amount <= 0)
            return;

        Vector2[] semiCirclePositions = GetSemiCircle(amount, size, location, origin);

        for (int i = 0; i < amount; i++)
        {
            var asteroid = asteroidPool.Acquire();

            if (asteroid == null)
                return;

            var asteroidSpeed = this.GetRandomInRange(asteroid.MinSpeed, asteroid.MaxSpeed);
            var asteroidDirection = GetDirection(amount, semiCirclePositions[i], location);
            var asteroidVelocity = asteroidSpeed * asteroidDirection;

            asteroid.Initialize(size, semiCirclePositions[i], asteroidVelocity);

            asteroid.AsteroidCollisionEvent.AddListener(OnAsteroidCollision);
        }
    }

    private Vector2 GetDirection(int amount, Vector2 position, Vector2 origin)
    {
        Vector2 initialDirection = (position - origin).normalized;
        float sliceAngle = SEMI_CIRCLE_DEG / amount;
        float rotationAngle = this.GetRandomInRange(-sliceAngle / 2, sliceAngle / 2);

        var result = this.RotateVectorByDeg(initialDirection, rotationAngle);
        return result;
    }

    private Vector2[] GetSemiCircle(int amount, int size, Vector2 position, Vector2 origin)
    {
        if (amount == 0)
            return null;

        Vector2[] result = new Vector2[amount];

        if (amount == 1)
        {
            result[0] = position;
            return result;
        }

        float sliceAngle = SEMI_CIRCLE_DEG / amount;
        float radius = GetSemiCircleRadius(sliceAngle, Mathf.Pow(2, size));

        Vector2 initialDirection = this.RotateVectorByDeg(position - origin, -SEMI_CIRCLE_DEG / 2).normalized;

        for (int i = 0; i < amount; i++)
        {
            Vector2 direction = this.RotateVectorByDeg(initialDirection, (i + 0.5f) * sliceAngle);
            Vector2 newPosition = position + direction * radius;

            result[i] = newPosition;
        }

        return result;
    }

    private float GetSemiCircleRadius(float sliceAngle, float radius)
    {
        float result = radius / Mathf.Sin(sliceAngle / 2);
        return result;
    }

    private void OnAsteroidCollision(Asteroid asteroid, Collider2D collision)
    {
        if (collision.tag == Tags.ASTEROID)
            return;

        if (collision.tag == Tags.PLAYER || collision.tag == Tags.PLAYER_BULLET)
        {
            ScoreManager.ScoreEvent.Invoke(Tags.ASTEROID, collision.tag);
        }

        asteroid.AsteroidCollisionEvent.RemoveListener(OnAsteroidCollision);

        asteroidPool.Release(asteroid);

        if (asteroid.Size > 1)
        {
            int newSize = asteroid.Size - 1;
            SpawnAsteroids(AsteroidData.NUM_SPLIT_ASTEROIDS, newSize, asteroid.Position, collision.transform.position);
        }

        if (GetActiveAsteroids() == 0)
        {
            AsteroidsClearedEvent.Invoke();
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