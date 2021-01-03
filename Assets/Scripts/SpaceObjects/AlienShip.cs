using UnityEngine;
using ExtensionMethods;

public class AlienShip : SpaceEntity
{
    private const int SCORE = 200;
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

    protected override void Update()
    {
        base.Update();

        if (!Gun.IsCoolingDown)
        {
            FireInRandomDirection();
        }
    }

    public override void OnCollision(Collider2D collision)
    {
        if (collision.tag == Tags.PLAYER_BULLET)
        {
            ScoreManager.Instance.AddScore(SCORE);
        }
    }

    private void FireInRandomDirection()
    {
        Vector2 firingDirection = this.GetRandomDirection();

        Vector3 gunPosition = new Vector3(Position.x, Position.y, 0) + new Vector3(firingDirection.x, firingDirection.y, 0) * BARREL_LENGTH;
        Gun.transform.position = gunPosition;

        Gun.Fire(firingDirection);
    }

    public override void Terminate()
    {
        Gun.Terminate();
    }
}
