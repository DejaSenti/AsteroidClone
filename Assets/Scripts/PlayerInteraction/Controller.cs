using UnityEngine;

[RequireComponent(typeof(PlayerShip))]
public class Controller : MonoBehaviour
{
    public PlayerInput PlayerInput;

    private PlayerShip playerShip;

    private void Awake()
    {
        playerShip = GetComponent<PlayerShip>();
    }

    void FixedUpdate()
    {
        if (GameManager.IsGamePaused)
            return;

        PlayerInput.UpdateInput();

        if (PlayerInput.leftButtonHeld)
        {
            playerShip.Rotate(RotationDirection.CCW);
        }
        if (PlayerInput.rightButtonHeld)
        {
            playerShip.Rotate(RotationDirection.CW);
        }
        if (PlayerInput.accelerateButtonHeld)
        {
            playerShip.Accelerate();
        }
        if (PlayerInput.fireButtonHeld)
        {
            playerShip.Fire();
        }
    }
}
