using UnityEngine;

public abstract class BaseMenu : MonoBehaviour
{
    protected void OnEnable()
    {
        AddButtonListeners();
    }

    public abstract void AddButtonListeners();
    public abstract void RemoveButtonListeners();

    protected void OnDisable()
    {
        RemoveButtonListeners();
    }
}