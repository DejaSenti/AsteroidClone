using System;
using UnityEngine;

public class Bullet : SpaceObject
{
    [NonSerialized]
    public float Lifetime;

    [SerializeField]
    private Timer distanceTimer;

    public void Shoot(Vector2 velocity)
    {
        RB.velocity = velocity;
        distanceTimer.StartTimer(Lifetime);
    }

    private void FixedUpdate()
    {
        if (distanceTimer.TimerElapsed)
        {
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        gameObject.SetActive(false);
        // animate something, play sound
    }
}
