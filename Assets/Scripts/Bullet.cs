using System;
using UnityEngine;

public class Bullet : SpaceObject
{
    [NonSerialized]
    public float Lifetime;

    [SerializeField]
    private Timer distanceTimer;

    private void Awake()
    {
        enabled = false;
    }

    public void Shoot(Vector2 velocity)
    {
        enabled = true;
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
        enabled = false;
        gameObject.SetActive(false);
        // animate something, play sound
    }
}
