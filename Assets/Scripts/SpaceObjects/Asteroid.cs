using UnityEngine;

public class Asteroid : SpaceEntity
{
    private const int SCORE = 100;

    public AsteroidCollisionEvent AsteroidCollisionEvent;

    public float MinSpeed;
    public float MaxSpeed;

    public int Size;

    protected override void Awake()
    {
        base.Awake();

        if (AsteroidCollisionEvent == null)
            AsteroidCollisionEvent = new AsteroidCollisionEvent();
    }

    public void Initialize(int size, Vector2 position, Vector2 velocity)
    {
        Size = size;

        var scale = Mathf.Pow(2, Size);
        var localScale = new Vector3(scale, scale, 1);
        transform.localScale = localScale;

        Position = position;

        RB.velocity = velocity;
    }

    public override void OnCollision(Collider2D collision)
    {
        var addedScore = 0;
        switch (collision.tag)
        {
            case Tags.ASTEROID:
                return;
            case Tags.PLAYER_BULLET:
                addedScore = SCORE * AsteroidData.MAX_ASTEROID_SIZE / Size;
                break;
            case Tags.PLAYER:
                addedScore = SCORE * AsteroidData.MAX_ASTEROID_SIZE / Size - (int) (SCORE * 0.5);
                break;
        }

        if (addedScore != 0)
            ScoreManager.Instance.AddScore(addedScore);

        if (AsteroidCollisionEvent != null && isActiveAndEnabled)
            AsteroidCollisionEvent.Invoke(this, collision);
    }
}

