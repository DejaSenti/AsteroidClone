﻿using UnityEngine;
using ExtensionMethods;

public class AlienShip : SpaceEntity
{
    private const float BARREL_LENGTH = 4;
    
    public float MinSpeed;
    public float MaxSpeed;

    public Gun Gun;

    public AlienShipCollisionEvent AlienShipCollisionEvent;

    protected override void Awake()
    {
        base.Awake();

        if (AlienShipCollisionEvent == null)
        {
            AlienShipCollisionEvent = new AlienShipCollisionEvent();
        }

        Gun.Initialize();
    }

    public void Initialize(Vector2 position, Vector2 velocity)
    {
        Position = position;
        RB.velocity = velocity;
    }

    protected override void Update()
    {
        base.Update();

        if (!Gun.IsCoolingDown)
        {
            FireInRandomDirection();
        }
    }

    private void FireInRandomDirection()
    {
        Vector2 firingDirection = this.GetRandomDirection();

        Vector3 gunPosition = new Vector3(Position.x, Position.y, 0) + new Vector3(firingDirection.x, firingDirection.y, 0) * BARREL_LENGTH;
        Gun.transform.position = gunPosition;

        Gun.Fire(firingDirection);
    }

    public override void OnCollision(Collider2D collision)
    {
        AlienShipCollisionEvent.Invoke(this, collision);
    }

    public override void Terminate()
    {
        Gun.Terminate();
    }
}
