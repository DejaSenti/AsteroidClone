using UnityEngine;

public class Timer : MonoBehaviour
{
    private float duration;
    private float timeElapsed;

    private bool TimerElapsed { get => timeElapsed > duration; }

    private void Awake()
    {
        ResetTimer();
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
        if (TimerElapsed)
        {
            ResetTimer();
        }
    }

    public void ResetTimer()
    {
        enabled = false;
        duration = 0;
        timeElapsed = 0;
    }
}
