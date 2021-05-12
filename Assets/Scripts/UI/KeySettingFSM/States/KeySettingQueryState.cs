using System;
using UnityEngine;

public class KeySettingQueryState : BaseKeySettingState
{
    private const string QUERY_TEXT_FORMAT = "Press new key for {0} binding or Escape to cancel";

    public override void OnEnter()
    {
        keySetting.Overlay.SetActive(true);
        keySetting.OverlayText.text = string.Format(QUERY_TEXT_FORMAT, keySetting.type.ToString());
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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
}
