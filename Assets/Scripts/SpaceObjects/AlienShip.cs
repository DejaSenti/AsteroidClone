using UnityEngine;
using ExtensionMethods;

public class AlienShip : SpaceObject
{
    private const float BARREL_LENGTH = 4;
    
    public float MinSpeed;
    public float MaxSpeed;

    public Gun Gun;

    void Start()
    {
        float speed = this.GetRandomInRange(MinSpeed, MaxSpeed);
        Vector2 direction = this.GetRandomDirection();

        RB.velocity = speed * direction;
    }

    private void Update()
    {
        if (!Gun.IsCoolingDown)
        {
            Vector2 firingDirection = this.GetRandomDirection();

            Vector3 gunPosition = new Vector3(transform.position.x, transform.position.y, 0) + new Vector3(firingDirection.x, firingDirection.y, 0) * BARREL_LENGTH;
            Gun.transform.position = gunPosition;

            Gun.Fire(firingDirection);
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
