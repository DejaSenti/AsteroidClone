using UnityEngine;


public class PlayerShip : SpaceObject
{
    public float MaxSpeed;
    public float Acceleration;
    public float AngularAcceleration;

    public Gun Gun;

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

    public override void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
