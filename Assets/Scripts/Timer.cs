using UnityEngine;
using UnityEngine.Events;

// TODO: Redo this with UnityEvents
public class Timer : MonoBehaviour
{
    public UnityEvent TimerElapsedEvent;

    private float duration;
    private float timeElapsed;

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
        if (timeElapsed > duration)
        {
            TimerElapsedEvent.Invoke();
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
