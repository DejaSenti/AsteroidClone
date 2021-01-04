using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ObjectPool<T> where T : SpaceEntity
{
    public int ActiveCount { get => activeObjects.Count; }

    private List<T> objectPool;
    private List<T> activeObjects;
    private string poolType;

    public ObjectPool()
    {
        poolType = typeof(T).Name;
        objectPool = new List<T>();
        activeObjects = new List<T>();
    }

    public void Initialize(int poolSize)
    {
        if (objectPool.Count > 0)
        {
            Terminate();
        }

        var gameObjectPrefab = Resources.Load<GameObject>(MainAssetPaths.POOL_OBJECTS_PATH + poolType);

        for (int i = 0; i < poolSize; i++)
        {
            var gameObject = Object.Instantiate(gameObjectPrefab);

            T spaceObject = gameObject.GetComponentInChildren<T>();

            spaceObject.Deactivate();

            spaceObject.transform.position = Vector3.zero;

            objectPool.Add(spaceObject);
        }
    }

    public void Terminate()
    {
        var allObjects = GetAllPooledObjects();

        objectPool.Clear();
        activeObjects.Clear();

        foreach (T spaceObject in allObjects)
        {
            spaceObject.Deactivate();
            Object.Destroy(spaceObject.gameObject);
        }
    }

    public List<T> GetAllPooledObjects()
    {
        var result = objectPool.Concat(activeObjects).ToList();
        return result;
    }

    public T Acquire()
    {
        if (objectPool.Count == 0)
            return null;

        T spawnedObject = objectPool[0];

        objectPool.RemoveAt(0);
        activeObjects.Add(spawnedObject);

        spawnedObject.Activate();

        return spawnedObject;
    }

    public void Release(T existingObject)
    {
        if (!activeObjects.Contains(existingObject))
            return;

        activeObjects.RemoveAt(activeObjects.IndexOf(existingObject));
        objectPool.Add(existingObject);

        existingObject.Deactivate();
    }
}