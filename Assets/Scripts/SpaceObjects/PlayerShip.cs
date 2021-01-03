using UnityEngine;


public class PlayerShip : SpaceEntity
{
    public float MaxSpeed;
    public float Acceleration;
    public float AngularAcceleration;

    public Gun Gun;

    public PlayerShipCollisionEvent PlayerShipCollisionEvent;

    protected override void Awake()
    {
        base.Awake();

        if (PlayerShipCollisionEvent == null)
        {
            PlayerShipCollisionEvent = new PlayerShipCollisionEvent();
        }
    }

    public void Initialize(Vector2 position)
    {
        Position = position;
    }

    public void Accelerate()
    {
        if (RB.velocity.magnitude <= MaxSpeed)
        {
            RB.AddForce(Direction * Acceleration);
        }
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

    protected override void OnCollision(Collider2D collision)
    {
        PlayerShipCollisionEvent.Invoke();
    }

    public override void Terminate()
    {
        Gun.Terminate();
    }
}
