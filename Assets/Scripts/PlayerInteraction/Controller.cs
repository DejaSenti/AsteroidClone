using UnityEngine;

[RequireComponent(typeof(PlayerShip))]
public class Controller : MonoBehaviour
{
    private GameSettings PlayerInput;
    private PlayerShip playerShip;

    private void Start()
    {
        playerShip = GetComponent<PlayerShip>();
        PlayerInput = UISettings.Settings;
    }

    void FixedUpdate()
    {
        if (GameManager.IsGamePaused)
            return;

        PlayerInput.UpdateInput();

        if (PlayerInput.LeftButtonHeld)
        {
            playerShip.Rotate(RotationDirection.CCW);
        }
        if (PlayerInput.RightButtonHeld)
        {
            playerShip.Rotate(RotationDirection.CW);
        }
        if (PlayerInput.AccelerateButtonHeld)
        {
            playerShip.Accelerate();
        }
        if (PlayerInput.FireButtonHeld)
        {
            playerShip.Fire();
        }
    }
}
