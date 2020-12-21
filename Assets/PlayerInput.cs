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
    public bool leftButtonDown;
    [NonSerialized]
    public bool leftButtonHeld;
    [NonSerialized]
    public bool rightButtonDown;
    [NonSerialized]
    public bool rightButtonHeld;
    [NonSerialized]
    public bool accelerateButtonDown;
    [NonSerialized]
    public bool accelerateButtonHeld;
    [NonSerialized]
    public bool fireButtonDown;
    [NonSerialized]
    public bool fireButtonHeld;

    public void UpdateInput()
    {
        leftButtonDown = Input.GetKeyDown(leftButton);
        leftButtonHeld = Input.GetKey(leftButton);
        rightButtonDown = Input.GetKeyDown(rightButton);
        rightButtonHeld = Input.GetKey(rightButton);
        accelerateButtonDown = Input.GetKeyDown(accelerateButton);
        accelerateButtonHeld = Input.GetKey(accelerateButton);
        fireButtonDown = Input.GetKeyDown(fireButton);
        fireButtonHeld = Input.GetKey(fireButton);
    }
}