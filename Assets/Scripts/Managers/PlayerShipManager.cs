using UnityEngine;

[RequireComponent(typeof(Timer))]
public class PlayerShipManager : MonoBehaviour
{
    private const float PLAYER_RESPAWN_DELAY = 1.5f;
    public int MaxPlayerHealth;

    private PlayerShip playerShip;
    private int playerHealth;

    public Timer PlayerRespawnDelayTimer;
    public PlayerHealthDisplay PlayerHealthDisplay;

    public PlayerDeathEvent PlayerDeathEvent;

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

    public void Initialize()
    {
        playerHealth = MaxPlayerHealth;
        UpdateHealthDisplay();

        SpawnPlayerShip();
    }

    private void UpdateHealthDisplay()
    {
        PlayerHealthDisplay.UpdateDisplay(playerHealth);
    }

    private void SpawnPlayerShip()
    {
        Vector2 position = Vector2.zero;
        playerShip.Position = position;
        playerShip.Direction = position;

        playerShip.Activate();
        playerShip.PlayerShipCollisionEvent.AddListener(OnPlayerShipCollision);
    }

    private void OnPlayerShipCollision()
    {
        playerShip.Deactivate();

        playerShip.PlayerShipCollisionEvent.RemoveListener(OnPlayerShipCollision);

        playerHealth--;
        UpdateHealthDisplay();

        if (playerHealth > 0)
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

    public void Terminate()
    {
        playerShip.PlayerShipCollisionEvent.RemoveAllListeners();

        PlayerRespawnDelayTimer.ResetTimer();
        PlayerRespawnDelayTimer.TimerElapsedEvent.RemoveAllListeners();

        TerminateSubordinates();
    }

    public void TerminateSubordinates()
    {
        playerShip.Deactivate();
    }
}