using System;
using UnityEngine;

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

    private void OnAsteroidCollisionEvent(Asteroid arg0, string arg1)
    {

    }

    private void Start()
    {
        for (int i = 0; i < Level; i++)
        {
            var asteroid = asteroidPool.Spawn();
            asteroid.Size = MAX_ASTEROID_SIZE;
            asteroid.RB.position = Vector2.zero;
        }
    

    void Update()
    {
        
    }
}
}
