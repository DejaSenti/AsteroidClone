using UnityEngine;

public abstract class BaseKeySettingState
{
    protected KeySetting keySetting;
    protected KeySettingFSM fsm;

    public void Initialize(KeySetting keySetting, KeySettingFSM fsm)
    {
        this.keySetting = keySetting;
        this.fsm = fsm;
    }

    public virtual void OnEnter(KeyCode key)
    {
        
    }

    public virtual void OnEnter()
    {
        
    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnExit()
    {

    }
}