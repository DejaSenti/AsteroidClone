using UnityEngine;
using ExtensionMethods;
using System;

public class Asteroid : SpaceObject
{
    public AsteroidCollisionEvent AsteroidCollisionEvent;

    public CircleCollider2D Collider;

    public float MinSpeed;
    public float MaxSpeed;

    [NonSerialized]
    public int Size;

    private float scale { get => Mathf.Pow(2, Size); }

    private void Start()
    {
        if (AsteroidCollisionEvent == null)
            AsteroidCollisionEvent = new AsteroidCollisionEvent();

        transform.localScale = new Vector3(scale, scale, 1);
        float speed = this.GetRandomInRange(MinSpeed, MaxSpeed);
        RB.velocity = this.GetRandomDirection() * speed;
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        AsteroidCollisionEvent.Invoke(this, collision.tag);
    }
}
