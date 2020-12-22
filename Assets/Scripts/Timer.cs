using UnityEngine;

public class Timer : MonoBehaviour
{
    private float duration;
    private float timeElapsed;

    private void Awake()
    {
        enabled = false;
    }

    public void StartTimer(float duration)
    {
        this.duration = duration;
        timeElapsed = 0;
        enabled = true;
    }

    private void FixedUpdate()
    {
        timeElapsed += Time.fixedDeltaTime;
        if (timeElapsed >= duration)
        {
            enabled = false;
        }
    }
}
