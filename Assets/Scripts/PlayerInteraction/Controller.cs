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

    void Update()
    {
        PlayerInput.UpdateInput();

        if (PlayerInput.leftButtonDown || PlayerInput.leftButtonHeld)
        {
            playerShip.Rotate(RotationDirection.CCW);
        }
        if (PlayerInput.rightButtonDown || PlayerInput.rightButtonHeld)
        {
            playerShip.Rotate(RotationDirection.CW);
        }
        if (PlayerInput.accelerateButtonDown || PlayerInput.accelerateButtonHeld)
        {
            playerShip.Accelerate();
        }
        if (PlayerInput.fireButtonDown || PlayerInput.fireButtonHeld)
        {
            playerShip.Fire();
        }
    }
}
