using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInput", menuName = "AsteroidClone/PlayerInput")]
[Serializable]
public class GameSettings : ScriptableObject
{
    public KeyCode LeftButton;
    public KeyCode RightButton;
    public KeyCode AccelerateButton;
    public KeyCode FireButton;

    public FullScreenMode ScreenLayout;
    public int ScreenResolution;
    public int Difficulty;

    [NonSerialized]
    public bool LeftButtonHeld;
    [NonSerialized]
    public bool RightButtonHeld;
    [NonSerialized]
    public bool AccelerateButtonHeld;
    [NonSerialized]
    public bool FireButtonHeld;


    public void UpdateInput()
    {
        LeftButtonHeld = Input.GetKey(LeftButton);
        RightButtonHeld = Input.GetKey(RightButton);
        AccelerateButtonHeld = Input.GetKey(AccelerateButton);
        FireButtonHeld = Input.GetKey(FireButton);
    }
}