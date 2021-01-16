using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInput", menuName = "AsteroidClone/PlayerInput")]
public class PlayerInput : ScriptableObject
{
    public KeyCode leftButton;
    public KeyCode rightButton;
    public KeyCode accelerateButton;
    public KeyCode fireButton;

    [NonSerialized]
    public bool leftButtonHeld;
    [NonSerialized]
    public bool rightButtonHeld;
    [NonSerialized]
    public bool accelerateButtonHeld;
    [NonSerialized]
    public bool fireButtonHeld;

    public void UpdateInput()
    {
        leftButtonHeld = Input.GetKey(leftButton);
        rightButtonHeld = Input.GetKey(rightButton);
        accelerateButtonHeld = Input.GetKey(accelerateButton);
        fireButtonHeld = Input.GetKey(fireButton);
    }
}