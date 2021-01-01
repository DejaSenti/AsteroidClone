using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string GAME_OVER_MESSAGE = "Game Over";

    public PlayerShipManager PlayerShipManager;
    public TextMeshProUGUI Announcements;

    private void Start()
    {
        Announcements.text = "";

        PlayerShipManager.Initialize();
        PlayerShipManager.PlayerDeathEvent.AddListener(OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        Announcements.text = GAME_OVER_MESSAGE;
    }
}