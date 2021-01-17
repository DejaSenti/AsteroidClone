using UnityEngine;
using ExtensionMethods;

public class AlienShip : SpaceEntity
{
    private const float SHIP_SIZE = 4;
    
    public float MinSpeed;
    public float MaxSpeed;

    public Gun Gun;

    public AlienShipCollisionEvent AlienShipCollisionEvent;

    private bool isOnScreen;
    private bool enteredScreen { get => Position.x >= -SpaceBoundary.Width / 2 + SHIP_SIZE
                                        && Position.x <= SpaceBoundary.Width / 2 - SHIP_SIZE
                                        && Position.y >= -SpaceBoundary.Height / 2 + SHIP_SIZE
                                        && Position.y <= SpaceBoundary.Height / 2 - SHIP_SIZE; }

    protected override void Awake()
    {
        base.Awake();

        if (AlienShipCollisionEvent == null)
        {
            AlienShipCollisionEvent = new AlienShipCollisionEvent();
        }
    }

    public void Initialize(Vector2 position, Vector2 velocity)
    {
        Position = position;
        RB.velocity = velocity;

        isOnScreen = false;

        Gun.Initialize();
    }

    protected override void Update()
    {
        if (isOnScreen)
        {
            base.Update();

            if (!Gun.IsCoolingDown)
            {
                FireInRandomDirection();
            }
        }
        else if (!isOnScreen && enteredScreen)
        {
            isOnScreen = true;
        }
    }

    private void FireInRandomDirection()
    {
        Vector2 firingDirection = this.GetRandomDirection();

        Vector3 gunPosition = new Vector3(Position.x, Position.y, 0) + new Vector3(firingDirection.x, firingDirection.y, 0) * SHIP_SIZE;
        Gun.transform.position = gunPosition;

        Gun.Fire(firingDirection);
    }

    public override void OnCollision(Collider2D collision)
    {
        AlienShipCollisionEvent.Invoke(this, collision);
    }

    public override void Terminate()
    {
        AlienShipCollisionEvent.RemoveAllListeners();
        Gun.Terminate();
    }
}
