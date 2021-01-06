using UnityEngine;

[RequireComponent(typeof(Timer))]
public class PlayerShipManager : MonoBehaviour
{
    private const float PLAYER_RESPAWN_DELAY = 1.5f;
    public int MaxPlayerHealth;

    private PlayerShip playerShip;
    public int PlayerHealth;

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
        PlayerHealth = MaxPlayerHealth;
        UpdateHealthDisplay();
    }

    public void SpawnPlayerShip()
    {
        ResetPlayerPosition();
        playerShip.Initialize();
        playerShip.PlayerShipCollisionEvent.AddListener(OnPlayerShipCollision);
    }

    private void OnPlayerShipCollision()
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

    private void ResetPlayerPosition()
    {
        playerShip.SetPositionAndRotation(Vector2.zero, Vector2.zero);
    }

    private void UpdateHealthDisplay()
    {
        PlayerHealthDisplay.UpdateDisplay(PlayerHealth);
    }

    public void Terminate()
    {
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