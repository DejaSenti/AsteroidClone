using System;
using UnityEngine;

public class Bullet : SpaceObject
{
    public BulletDestroyedEvent BulletDestroyedEvent;

    [NonSerialized]
    public float Lifetime;

    private Timer distanceTimer;

    private void Awake()
    {
        if (BulletDestroyedEvent == null)
            BulletDestroyedEvent = new BulletDestroyedEvent();

        distanceTimer = GetComponent<Timer>();
        distanceTimer.TimerElapsedEvent.AddListener(OnTimerElasped);
    }

    public void Shoot(Vector2 velocity)
    {
        RB.velocity = velocity;
        distanceTimer.StartTimer(Lifetime);
    }

    private void OnTimerElasped()
    {
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        distanceTimer.ResetTimer();
        BulletDestroyedEvent.Invoke(this);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        DestroyBullet();
    }
}
