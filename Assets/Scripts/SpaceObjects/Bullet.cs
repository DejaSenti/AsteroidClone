using System;
using UnityEngine;

public class Bullet : SpaceObject
{
    [NonSerialized]
    public float Lifetime;

    private Timer distanceTimer;

    private void Awake()
    {
        distanceTimer = GetComponent<Timer>();
        distanceTimer.TimerElapsedEvent.AddListener(OnTimerElasped);
    }

    private void OnTimerElasped()
    {
        DestroyBullet();
    }

    public void Shoot(Vector2 velocity)
    {
        RB.velocity = velocity;
        distanceTimer.StartTimer(Lifetime);
    }

    private void DestroyBullet()
    {
        gameObject.SetActive(false);
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        throw new NotImplementedException();
    }
}