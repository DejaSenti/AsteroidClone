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

    public void Initialize(int poolSize, Transform parent)
    {
        if (objectPool.Count > 0)
        {
            var allObjects = objectPool.Concat(activeObjects).ToList();
            objectPool.Clear();
            activeObjects.Clear();

            foreach (T spaceObject in allObjects)
            {
                Object.Destroy(spaceObject.gameObject);
            }
        }

        var gameObjectPrefab = Resources.Load<GameObject>(MainAssetPaths.POOL_OBJECTS_PATH + poolType);

        for (int i = 0; i < poolSize; i++)
        {
            var gameObject = Object.Instantiate(gameObjectPrefab);

            gameObject.SetActive(false);

            T spaceObject = gameObject.GetComponentInChildren<T>();

            objectPool.Add(spaceObject);
        }
    }

    public T Spawn()
    {
        if (objectPool.Count == 0)
            return null;

        T spawnedObject = objectPool[0];

        objectPool.RemoveAt(0);
        activeObjects.Add(spawnedObject);

        spawnedObject.gameObject.SetActive(true);

        return spawnedObject;
    }

    public void Kill(T existingObject)
    {
        if (!activeObjects.Contains(existingObject))
            return;

        activeObjects.RemoveAt(activeObjects.IndexOf(existingObject));
        objectPool.Add(existingObject);

        existingObject.gameObject.SetActive(false);
    }
}