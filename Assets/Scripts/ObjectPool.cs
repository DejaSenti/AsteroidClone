﻿using UnityEngine;
using System.Collections.Generic;

public class ObjectPool<T> where T : SpaceObject
{
    public int ActiveCount { get => CountActiveObjects(); }

    private List<T> objectPool;
    private string poolType;

    public ObjectPool()
    {
        poolType = typeof(T).Name;
        objectPool = new List<T>();
    }

    public void Initialize(int poolSize, Transform parent)
    {
        for (int i = 0; i < poolSize; i++)
        {
            var gameObject = Resources.Load<GameObject>(MainAssetPaths.POOL_OBJECTS_PATH + poolType);
            Object.Instantiate(gameObject, parent);
            gameObject.SetActive(false);

            T spaceObject = gameObject.GetComponent<T>();
            objectPool.Add(spaceObject);
        }
    }

    public T Spawn()
    {
        foreach(T spaceObject in objectPool)
        {
            if (!spaceObject.gameObject.activeInHierarchy)
            {
                return spaceObject;
            }
        }

        return null;
    }

    private int CountActiveObjects()
    {
        int activeObjects = 0;

        foreach(T spaceObject in objectPool)
        {
            if (spaceObject.gameObject.activeInHierarchy)
                activeObjects++;
        }

        return activeObjects;
    }
}