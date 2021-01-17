using System.Linq;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public static SpawnPointManager Instance;

    private const string DEFAULT_LAYER_NAME = "Default";

#pragma warning disable 0649
    [SerializeField]
    private BoxCollider2D[] playerSpawnPoints = new BoxCollider2D[9];
    [SerializeField]
    private BoxCollider2D[] asteroidSpawnPoints = new BoxCollider2D[8];
    [SerializeField]
    private BoxCollider2D[] alienShipSpawnPoints = new BoxCollider2D[12];
    [SerializeField]
    private Vector2[] asteroidSpawnLocations = new Vector2[8];
    [SerializeField]
    private Vector2[] alienShipSpawnLocations = new Vector2[12];
#pragma warning restore 0649

    private Vector2 screenSize;

    private void Start()
    {
        Instance = this;
    }

    public void Initialize()
    {
        UpdateScreenSize();
        PlaceSpawnPoints();
    }

    public Vector2? GetPlayerSpawnPoint()
    {
        var spawnPoint = GetSpawnPoint(playerSpawnPoints);

        Vector2? result = spawnPoint != null ? (Vector2?) spawnPoint.offset : null;

        return result;
    }

    public Vector2? GetAlienShipSpawnPoint()
    {
        var spawnPoint = GetSpawnPoint(alienShipSpawnPoints);

        Vector2? result = spawnPoint != null ? (Vector2?) spawnPoint.offset : null;

        return result;
    }

    public Vector2? GetAsteroidSpawnPoint()
    {
        var spawnPoint = GetSpawnPoint(asteroidSpawnPoints);

        Vector2? result = spawnPoint != null ? (Vector2?) spawnPoint.offset : null;

        return result;
    }

    private BoxCollider2D GetSpawnPoint(BoxCollider2D[] spawnPoints)
    {
        System.Random r = new System.Random();
        spawnPoints = spawnPoints.OrderBy(x => r.Next()).ToArray();

        foreach(BoxCollider2D spawnPoint in spawnPoints)
        {
            if (!spawnPoint.IsTouchingLayers(LayerMask.GetMask(DEFAULT_LAYER_NAME)))
            {
                return spawnPoint;
            }
        }

        return null;
    }

    private void Update()
    {
        if (screenSize.x != SpaceBoundary.Width || screenSize.y != SpaceBoundary.Height)
        {
            UpdateScreenSize();
            PlaceSpawnPoints();
        }
    }

    private void UpdateScreenSize()
    {
        screenSize = new Vector2(SpaceBoundary.Width, SpaceBoundary.Height);
    }

    private void PlaceSpawnPoints()
    {
        for (int i = 0; i < alienShipSpawnPoints.Length; i++)
        {
            var offsetX = alienShipSpawnLocations[i].x * (screenSize.x + alienShipSpawnPoints[i].size.x) / 2;
            var offsetY = alienShipSpawnLocations[i].y * (screenSize.y + alienShipSpawnPoints[i].size.y) / 2;
            alienShipSpawnPoints[i].offset = new Vector2(offsetX, offsetY);
        }

        for (int i = 0; i < asteroidSpawnPoints.Length; i++)
        {
            var offsetX = asteroidSpawnLocations[i].x * (screenSize.x - asteroidSpawnPoints[i].size.x) / 2;
            var offsetY = asteroidSpawnLocations[i].y * (screenSize.y - asteroidSpawnPoints[i].size.y) / 2;
            asteroidSpawnPoints[i].offset = new Vector2(offsetX, offsetY);
        }
    }
}
