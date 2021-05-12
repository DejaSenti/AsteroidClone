using System;
using UnityEngine;

public class KeySettingWriteState : BaseKeySettingState
{
    private const string OVERWRITE_TEXT_FORMAT = "Key {0} is already taken by {1}!\n" +
        "Press {0} again to overwrite (this will unbind {1})\n\n" +
        "Press different key to try again or Escape to cancel";

    private KeySettingType settingToReset;

    private KeyCode keyToWrite;

    private KeyCode newRight;
    private KeyCode newLeft;
    private KeyCode newAccelerate;
    private KeyCode newFire;

    public override void OnEnter(KeyCode keyCode)
    {
        keyToWrite = keyCode;

        newRight = MainMenuEntry.Settings.RightButton;
        newLeft = MainMenuEntry.Settings.LeftButton;
        newAccelerate = MainMenuEntry.Settings.AccelerateButton;
        newFire = MainMenuEntry.Settings.FireButton;

        bool isOverwriting = false;

        switch (keySetting.type)
        {
            case KeySettingType.RotateCW:
                newRight = keyToWrite;
                break;

            case KeySettingType.RotateCCW:
                newLeft = keyToWrite;
                break;

            case KeySettingType.Accelerate:
                newAccelerate = keyToWrite;
                break;

            case KeySettingType.Fire:
                newFire = keyToWrite;
                break;

            default:
                Debug.Log("Not a valid key setting type!");
                fsm.SetState<KeySettingIdleState>();
                return;
        }

        if (MainMenuEntry.Settings.RightButton == keyToWrite && keySetting.type != KeySettingType.RotateCW)
        {
            settingToReset = KeySettingType.RotateCW;
            isOverwriting = true;
        }

        if (MainMenuEntry.Settings.LeftButton == keyToWrite && keySetting.type != KeySettingType.RotateCCW)
        {
            settingToReset = KeySettingType.RotateCCW;
            isOverwriting = true;
        }

        if (MainMenuEntry.Settings.AccelerateButton == keyToWrite && keySetting.type != KeySettingType.Accelerate)
        {
            settingToReset = KeySettingType.Accelerate;
            isOverwriting = true;
        }

        if (MainMenuEntry.Settings.FireButton == keyToWrite && keySetting.type != KeySettingType.Fire)
        {
            settingToReset = KeySettingType.Fire;
            isOverwriting = true;
        }

        if (!isOverwriting)
        {
            WriteNewKeys();
            fsm.SetState<KeySettingIdleState>();
            return;
        }

        keySetting.Overlay.SetActive(true);
        keySetting.OverlayText.text = string.Format(OVERWRITE_TEXT_FORMAT, UIHelpers.KeycodeToText(keyToWrite), UIHelpers.KeySettingToText(settingToReset));
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            fsm.SetState<KeySettingIdleState>();
            return;
        }

        if (Input.GetKeyDown(keyToWrite))
        {
            switch (settingToReset)
            {
                case KeySettingType.RotateCW:
                    newRight = KeyCode.None;
                    break;

                case KeySettingType.RotateCCW:
                    newLeft = KeyCode.None;
                    break;

                case KeySettingType.Accelerate:
                    newAccelerate = KeyCode.None;
                    break;

                case KeySettingType.Fire:
                    newFire = KeyCode.None;
                    break;
            }

            WriteNewKeys();
            fsm.SetState<KeySettingIdleState>();
            return;
        }

        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    fsm.SetState<KeySettingWriteState>(keyCode);
                    return;
                }
            }
        }
    }

    public override void OnExit()
    {
        keySetting.OverlayText.text = "";
        keySetting.Overlay.SetActive(false);
    }

    private void WriteNewKeys()
    {
        MainMenuEntry.Settings.RightButton = newRight;
        MainMenuEntry.Settings.LeftButton = newLeft;
        MainMenuEntry.Settings.AccelerateButton = newAccelerate;
        MainMenuEntry.Settings.FireButton = newFire;

        keySetting.KeyChangedEvent.Invoke();
    }
}