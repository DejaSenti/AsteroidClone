using UnityEngine;
using ExtensionMethods;

public class AsteroidManager : MonoBehaviour
{
    private const int MAX_ASTEROID_SIZE = 3;
    private const int NUM_SPLIT_ASTEROIDS = 2;
    private const float DISTANCE_SAFETY_MOD = 1.5f;

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

        SpawnAsteroids(Level, MAX_ASTEROID_SIZE, SpaceBoundary.Width / 2 * Vector2.right);
    }

    private void OnAsteroidCollisionEvent(Asteroid asteroid, Collider2D collision)
    {
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
        for (int i = 0; i < amount; i++)
        {
            var asteroid = asteroidPool.Spawn();

            if (asteroid == null)
                return;

            var asteroidSpeed = this.GetRandomInRange(asteroid.MinSpeed, asteroid.MaxSpeed);
            var asteroidDirection = this.GetRandomDirection();
            var asteroidVelocity = asteroidSpeed * asteroidDirection;

            asteroid.Initialize(size, location, asteroidVelocity);

            asteroid.AsteroidCollisionEvent.AddListener(OnAsteroidCollisionEvent);
        }
    }

    [ContextMenu("Restart Level")]
    public void RestartLevel()
    {
        Initialize();
    }

    public Vector2[] GetPositionArrayAroundLocation(int amount, float objectRadius, Vector2 location)
    {
        if (amount == 0)
            return null;

        Vector2[] result = new Vector2[amount];

        if (amount == 1)
        {
            result[0] = location;
            return result;
        }

        float sliceAngle = MathExtensions.FULL_CIRCLE_DEG / amount;
        float initialAngle = this.GetRandomAngle();

        float distance = objectRadius / Mathf.Sin(Mathf.Deg2Rad * sliceAngle / 2) * DISTANCE_SAFETY_MOD;

        for (int i = 0; i < amount; i++)
        {
            Vector2 position = location + distance * this.RotationToVector2(initialAngle + i * sliceAngle);
            result[i] = position;
        }

        return result;
    }

    public Vector2 GetDirectionInRangeRelatedToPosition(int amount, Vector2 centerPosition, Vector2 objectPosition)
    {
        float angleRange = MathExtensions.FULL_CIRCLE_DEG / amount;

        Vector2 generalDirection = (objectPosition - centerPosition).normalized;

        float angle = this.GetRandomInRange(-angleRange / 2, angleRange / 2);

        Vector2 result = this.RotateVectorByDeg(generalDirection, angle);

        return result;
    }
}
