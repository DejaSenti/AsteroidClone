using UnityEngine;

public class Asteroid : SpaceObject
{
    public AsteroidCollisionEvent AsteroidCollisionEvent;

    public float MinSpeed;
    public float MaxSpeed;

    public int Size;

    private void Awake()
    {
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
        if (AsteroidCollisionEvent != null && isActiveAndEnabled)
            AsteroidCollisionEvent.Invoke(this, collision);
    }
}
