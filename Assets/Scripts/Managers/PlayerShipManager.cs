using UnityEngine;

[RequireComponent(typeof(Timer))]
public class PlayerShipManager : MonoBehaviour
{
    private const float PLAYER_RESPAWN_DELAY = 1.5f;
    public int MaxPlayerHealth;

    public PlayerShip playerShip { get; private set; }
    public int PlayerHealth;

    public Timer PlayerRespawnDelayTimer;
    public PlayerHealthDisplay PlayerHealthDisplay;

    public PlayerDeathEvent PlayerDeathEvent;

    private bool isWaitingForRespawn;

# pragma warning disable 0649
    [SerializeField]
    private ParticleSystem ThrusterParticleSystem;
# pragma warning restore 0649

    private void Awake()
    {
        if (playerShip == null)
        {
            var playerShipGO = Resources.Load(MainAssetPaths.PLAYER_SHIP);
            var playerShipInstance = Instantiate(playerShipGO) as GameObject;

            playerShip = playerShipInstance.GetComponent<PlayerShip>();
        }

        playerShip.Deactivate();

        if (PlayerDeathEvent == null)
        {
            PlayerDeathEvent = new PlayerDeathEvent();
        }
    }

    private void Update()
    {
        if (isWaitingForRespawn)
        {
            SpawnPlayerShip();
        }
    }

    public void Initialize()
    {
        PlayerHealth = MaxPlayerHealth;
        UpdateHealthDisplay();

        playerShip.ThrusterParticleSystem = ThrusterParticleSystem;

        isWaitingForRespawn = false;
    }

    public void SpawnPlayerShip()
    {
        if (ResetPlayerPosition())
        {
            playerShip.Initialize();
            playerShip.PlayerShipCollisionEvent.AddListener(OnPlayerShipCollision);

            isWaitingForRespawn = false;
        }
        else
        {
            isWaitingForRespawn = true;
        }
    }

    private void OnPlayerShipCollision(SpaceEntity spaceEntity, string destroyerTag)
    {
        playerShip.PlayerShipCollisionEvent.RemoveListener(OnPlayerShipCollision);

        playerShip.Terminate();

        PlayerHealth--;
        UpdateHealthDisplay();

        if (PlayerHealth > 0)
        {
            PlayerRespawnDelayTimer.StartTimer(PLAYER_RESPAWN_DELAY);
            PlayerRespawnDelayTimer.TimerElapsedEvent.AddListener(OnPlayerRespawnTimerElapsed);
        }
        else
        {
            PlayerDeathEvent.Invoke();
        }
    }

    private void OnPlayerRespawnTimerElapsed()
    {
        SpawnPlayerShip();
        PlayerRespawnDelayTimer.TimerElapsedEvent.RemoveListener(OnPlayerRespawnTimerElapsed);
    }

    private bool ResetPlayerPosition()
    {
        Vector2? position_ = SpawnPointManager.Instance.GetPlayerSpawnPoint();
        Vector2 position;

        if (position_ == null)
            return false;

        position = (Vector2)position_;

        playerShip.SetPositionAndRotation(position, Vector2.zero);

        return true;
    }

    private void UpdateHealthDisplay()
    {
        PlayerHealthDisplay.UpdateDisplay(PlayerHealth);
    }

    public void Terminate()
    {
        isWaitingForRespawn = false;

        playerShip.PlayerShipCollisionEvent.RemoveListener(OnPlayerShipCollision);

        PlayerDeathEvent.RemoveAllListeners();

        PlayerRespawnDelayTimer.ResetTimer();
        PlayerRespawnDelayTimer.TimerElapsedEvent.RemoveAllListeners();

        TerminateSubordinates();
    }

    public void TerminateSubordinates()
    {
        playerShip.Terminate();
        PlayerHealthDisplay.Clear();
    }
}