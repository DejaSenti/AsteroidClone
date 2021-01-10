﻿using UnityEngine;

public class PlayerShip : SpaceEntity
{
    public float MaxSpeed;
    public float Acceleration;
    public float AngularAcceleration;

    public Gun Gun;

    public ParticleSystem ThrusterParticleSystem;
    private ParticleSystem.EmitParams emitParams;

    public PlayerShipCollisionEvent PlayerShipCollisionEvent;

    protected override void Awake()
    {
        base.Awake();

        if (PlayerShipCollisionEvent == null)
        {
            PlayerShipCollisionEvent = new PlayerShipCollisionEvent();
        }
    }

    public void Initialize()
    {
        Activate();
        Gun.Initialize();
    }

    public void SetPositionAndRotation(Vector2 position, Vector2 direction)
    {
        Position = position;
        Direction = direction;
    }

    public void Accelerate()
    {
        if (RB.velocity.magnitude <= MaxSpeed)
        {
            RB.AddForce(Direction * Acceleration);
        }

        ThrusterParticleSystem.transform.SetPositionAndRotation(CorporealForm.transform.position, CorporealForm.transform.rotation);
        ThrusterParticleSystem.Emit(20);
        ThrusterParticleSystem.Play();
    }

    public void Rotate(RotationDirection rotationDirection)
    {
        switch (rotationDirection)
        {
            case RotationDirection.CCW:
                RB.AddTorque(AngularAcceleration);
            break;

            case RotationDirection.CW:
                RB.AddTorque(-AngularAcceleration);
                break;

            default:
                return;
        }
    }

    public void Fire()
    {
        Gun.Fire(Direction);
    }

    public override void OnCollision(Collider2D collision)
    {
        PlayerShipCollisionEvent.Invoke();
    }

    public override void Terminate()
    {
        Deactivate();
        Gun.Terminate();
        PlayerShipCollisionEvent.RemoveAllListeners();
    }
}
