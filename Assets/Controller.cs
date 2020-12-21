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

    // Update is called once per frame
    void Update()
    {
        PlayerInput.UpdateInput();

        if (PlayerInput.leftButtonDown || PlayerInput.leftButtonHeld)
        {
            playerShip.RB.AddTorque(playerShip.AngularAcceleration);
        }
        if (PlayerInput.rightButtonDown || PlayerInput.rightButtonHeld)
        {
            playerShip.RB.AddTorque(-playerShip.AngularAcceleration);
        }
        if (PlayerInput.accelerateButtonDown || PlayerInput.accelerateButtonHeld)
        {
            playerShip.RB.AddForce(playerShip.Direction * playerShip.Acceleration);
        }
    }
}
