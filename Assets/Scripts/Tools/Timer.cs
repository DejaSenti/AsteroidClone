﻿using UnityEngine;
using UnityEngine.Events;

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

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > duration)
        {
            if (TimerElapsedEvent != null)
                TimerElapsedEvent.Invoke();
            ResetTimer();
        }
    }

    private void OnDisable()
    {
        ResetTimer();
    }

    public void ResetTimer()
    {
        enabled = false;
        duration = 0;
        timeElapsed = 0;
    }
}
