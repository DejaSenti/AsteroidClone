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
    }

    public void Shoot(Vector2 velocity)
    {
        gameObject.SetActive(true);
        RB.velocity = velocity;
        distanceTimer.StartTimer(Lifetime);
    }

    private void FixedUpdate()
    {
        if (!distanceTimer.enabled)
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
