using System;
using System.Collections.Generic;
using UnityEngine;

public class KeySettingFSM
{
    public readonly Dictionary<Type, BaseKeySettingState> StatesByType;
    protected BaseKeySettingState CurrentState;

    private KeySetting keySetting;

    public KeySettingFSM(KeySetting keySetting)
    {
        this.keySetting = keySetting;
        StatesByType = new Dictionary<Type, BaseKeySettingState>();
    }

    public void AddState(BaseKeySettingState state)
    {
        StatesByType[state.GetType()] = state;
        state.Initialize(keySetting, this);
    }

    public void SetState<NewState>(KeyCode key)
    {
        SetCurrentState<NewState>();

        CurrentState.OnEnter(key);
    }

    public void SetState<NewState>()
    {
        SetCurrentState<NewState>();

        CurrentState.OnEnter();
    }

    private void SetCurrentState<NewState>()
    {
        if (CurrentState != null)
        {
            CurrentState.OnExit();
        }

        CurrentState = StatesByType[typeof(NewState)];
    }

    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.OnUpdate();
        }
    }
}
