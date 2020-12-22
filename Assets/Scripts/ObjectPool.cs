using UnityEngine;
using System.Collections.Generic;

public class ObjectPool<T> where T : SpaceObject
{
    public int ActiveCount { get => CountActiveObjects(); }

    private List<T> objectPool;

    public ObjectPool()
    {
        objectPool = new List<T>();
    }

    public void Initialize(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            var gameObject = Resources.Load<GameObject>("Assets/Prefabs/PoolObjects/" + typeof(T));
            Object.Instantiate(gameObject);
            gameObject.SetActive(false);

            T spaceObject = gameObject.GetComponent<T>();
            objectPool.Add(spaceObject);
        }
    }

    public T Spawn()
    {
        foreach(T spaceObject in objectPool)
        {
            if (spaceObject.enabled)
                return spaceObject;
        }

        return null;
    }

    private int CountActiveObjects()
    {
        int activeObjects = 0;

        foreach(T spaceObject in objectPool)
        {
            if (spaceObject.enabled)
                activeObjects++;
        }

        return activeObjects;
    }
}