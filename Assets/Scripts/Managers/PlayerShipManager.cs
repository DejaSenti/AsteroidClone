using TMPro;
using UnityEngine;

public class PlayerShipManager : MonoBehaviour
{
    private const char HEALTH_LETTER = 'Y';

    public int MaxPlayerHealth;
    public float PlayerRespawnDelay;

    public TextMeshProUGUI HealthDisplayText;

    public PlayerDeathEvent PlayerDeathEvent;

    public Timer PlayerRespawnDelayTimer;

    private PlayerShip playerShip;
    public int playerHealth;

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

        playerShip.PlayerShipCollisionEvent.AddListener(OnPlayerShipCollision);
    }

    private void SpawnPlayerShip()
    {
        if (playerShip == null)
            return;

        Vector2 position = Vector2.zero;
        playerShip.Initialize(position);

        playerShip.Activate();
    }

    private void OnPlayerShipCollision()
    {
        playerShip.Deactivate();

        if (playerHealth >= 1)
        {
            playerHealth--;
            UpdateHealthDisplay();

            PlayerRespawnDelayTimer.StartTimer(PlayerRespawnDelay);
            PlayerRespawnDelayTimer.TimerElapsedEvent.AddListener(OnPlayerRespawnTimerElapsed);
        }
        else
        {
            playerShip.PlayerShipCollisionEvent.RemoveListener(OnPlayerShipCollision);

            PlayerDeathEvent.Invoke();
        }
    }

    private void OnPlayerRespawnTimerElapsed()
    {
        SpawnPlayerShip();
        PlayerRespawnDelayTimer.TimerElapsedEvent.RemoveListener(OnPlayerRespawnTimerElapsed);
    }

    private void UpdateHealthDisplay()
    {
        var playerHealthDisplay = new string(HEALTH_LETTER, playerHealth);
        HealthDisplayText.text = playerHealthDisplay;
    }
}
