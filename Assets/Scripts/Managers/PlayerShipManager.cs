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
    private int playerHealth;

    private void Awake()
    {
        if(PlayerDeathEvent == null)
        {
            PlayerDeathEvent = new PlayerDeathEvent();
        }
    }

    public void Initialize()
    {
        var playerShipGO = Resources.Load(MainAssetPaths.PLAYER_SHIP);
        var playerShipInstance = Instantiate(playerShipGO) as GameObject;

        playerShipInstance.SetActive(false);

        playerShip = playerShipInstance.GetComponent<PlayerShip>();
        playerHealth = MaxPlayerHealth;

        UpdateHealthDisplay();

        SpawnPlayerShip();
    }

    private void SpawnPlayerShip()
    {
        if (playerShip == null)
            return;

        Vector2 position = Vector2.zero;
        playerShip.Initialize(position);

        playerShip.PlayerShipCollisionEvent.AddListener(OnPlayerShipCollision);

        playerShip.gameObject.SetActive(true);
    }

    private void OnPlayerShipCollision()
    {
        playerShip.gameObject.SetActive(false);

        if (playerHealth >= 1)
        {
            playerHealth--;
            UpdateHealthDisplay();

            PlayerRespawnDelayTimer.StartTimer(PlayerRespawnDelay);
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
    }

    private void UpdateHealthDisplay()
    {
        var playerHealthDisplay = new string(HEALTH_LETTER, playerHealth);
        HealthDisplayText.text = playerHealthDisplay;
    }
}
